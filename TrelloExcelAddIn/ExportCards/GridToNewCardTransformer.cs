using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public class GridToNewCardTransformer : ICreateNewCards
	{
		public IEnumerable<NewCard> CreateCards(IListId list)
		{
			var grid = new Grid();

			foreach (Range cell in GetSelectedRange())			
				grid.AddCell(cell.Column, cell.Row, Convert.ToString(cell.Value));			

			return CreateCards(grid, list);
		}

		public IEnumerable<NewCard> CreateCards(Grid grid, IListId list)
		{
			if(!grid.Cells.Any())
				return Enumerable.Empty<NewCard>();

			var leftMostColumn = grid.Cells.Min(c => c.Column);

			return grid.Cells
				.GroupBy(c => c.Row)
				.Where(c => c.Any(x => x.Column == leftMostColumn))
				.Select(c =>
				{
					var newCard = new NewCard(c.First().Value, list);
					if (c.Count() > 1)
						newCard.Desc = c.ElementAt(1).Value;
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