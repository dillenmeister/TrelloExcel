using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using TrelloNet;

namespace TrelloExcelAddIn
{
	/// <summary>
	/// This is horrible and there are no tests. Sorry.
	/// </summary>
	internal class SelectedRangeToCardsTransformer : ISelectedRangeToCardsTransformer
	{
		public IEnumerable<NewCard> GetCards(IListId listId)
		{			
			var range = GetSelectedRange();
			if(range == null)
				return Enumerable.Empty<NewCard>();

			var cells = ConvertRangeToDictionary(range);

			var columns = GetAllColumns(cells);
			var leftMostColumn = GetTheLeftMostColumn(columns);
			var secondLeftMostColumn = GetTheSecondLeftMostColumn(columns);

			var cardsToAdd = new List<NewCard>();
			foreach (var cell in AllLeftMostCells(leftMostColumn, cells))
			{
				var name = GetName(cell);
				string description = null;
				if (ThereIsADescription(cell, cells, secondLeftMostColumn))
					description = GetDescription(cell, secondLeftMostColumn, cells);

				cardsToAdd.Add(new NewCard(name, listId) { Desc = description });
			}

			return cardsToAdd;
		}

		private static string GetName(KeyValuePair<Cell, string> cell)
		{
			return cell.Value;
		}

		private static string GetDescription(KeyValuePair<Cell, string> cell, int secondLeftMostColumn, Dictionary<Cell, string> cells)
		{
			return cells[new Cell(secondLeftMostColumn, cell.Key.Row)];
		}

		private static bool ThereIsADescription(KeyValuePair<Cell, string> cell, Dictionary<Cell, string> cells, int secondLeftMostColumn)
		{
			return secondLeftMostColumn > 0 && cells.ContainsKey(new Cell(secondLeftMostColumn, cell.Key.Row));
		}

		private static IEnumerable<KeyValuePair<Cell, string>> AllLeftMostCells(int leftMostColumn, Dictionary<Cell, string> cells)
		{
			return cells.Where(c => c.Key.Column == leftMostColumn);
		}

		private static int GetTheSecondLeftMostColumn(IOrderedEnumerable<int> columns)
		{
			return columns.Count() > 1 ? columns.ElementAt(1) : 0;
		}

		private static int GetTheLeftMostColumn(IEnumerable<int> columns)
		{
			return columns.ElementAt(0);
		}

		private static IOrderedEnumerable<int> GetAllColumns(Dictionary<Cell, string> cells)
		{
			return cells.Select(c => c.Key.Column).Distinct().OrderBy(c => c);
		}

		private static Dictionary<Cell, string> ConvertRangeToDictionary(Range range)
		{
			var cells = range.Cells.Cast<Range>()
				.Where(cell => Convert.ToString(cell.Value) != null)
				.ToDictionary<Range, Cell, string>(cell => new Cell(cell.Column, cell.Row), cell => Convert.ToString(cell.Value));
			return cells;
		}

		private static Range GetSelectedRange()
		{
			var selectedRange = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;

			var formulas = GetSpecialCells(selectedRange, XlCellType.xlCellTypeFormulas);
			var constants = GetSpecialCells(selectedRange, XlCellType.xlCellTypeConstants);

			if(formulas != null && constants != null)
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

		internal class Cell
		{
			public Cell(int column, int row)
			{
				Column = column;
				Row = row;
			}

			public int Column { get; set; }
			public int Row { get; set; }

			public override string ToString()
			{
				return string.Format("{0}, {1}", Column, Row);
			}

			public bool Equals(Cell other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return other.Column == Column && other.Row == Row;
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof(Cell)) return false;
				return Equals((Cell)obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return (Column * 397) ^ Row;
				}
			}
		}
	}
}