using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public class GridToNewCardTransformer : ICreateNewCards
	{
		public IEnumerable<CardInfo> CreateCards(IListId list)
		{
			var grid = new Grid();

			foreach (Range cell in GetSelectedRange())			
				grid.AddCell(cell.Column, cell.Row, Convert.ToString(cell.Value), cell.Value.GetType());			

			return CreateCards(grid, list);
		}

		public IEnumerable<CardInfo> CreateCards(Grid grid, IListId list)
		{
			if(!grid.Cells.Any())
				return Enumerable.Empty<CardInfo>();

			var leftMostColumn = grid.Cells.Min(c => c.Column);

			return grid.Cells
				.GroupBy(c => c.Row)
				.Where(c => c.Any(x => x.Column == leftMostColumn))
				.Select(c =>
				{
					var newCard = new CardInfo { Name = c.First().Value, ListId = list };
					var skipFirstColumn = c.Skip(1);

					var firstDateColumn = skipFirstColumn.FirstOrDefault(dc => dc.Type == typeof (DateTime));
					if (firstDateColumn != null)
						newCard.Due = DateTime.Parse(firstDateColumn.Value);

					var firstStringColumn = skipFirstColumn.FirstOrDefault(sc => sc.Type == typeof (string));
					if(firstStringColumn != null)
						newCard.Desc = firstStringColumn.Value;

					return newCard;
				});				
		}

		private static Range GetSelectedRange()
		{
			var selectedRange = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;

			var formulas = GetSpecialCells(selectedRange, XlCellType.xlCellTypeFormulas);
			var constants = GetSpecialCells(selectedRange, XlCellType.xlCellTypeConstants);

			if (formulas != null && constants != null)
				return Globals.ThisAddIn.Application.Union(formulas, constants);

			return formulas ?? constants;
		}

		private static Range GetSpecialCells(Range selectedRange, XlCellType type)
		{
			try
			{
				return selectedRange.SpecialCells(type);
			}
			catch
			{
				return null;
			}
		}
	}
}