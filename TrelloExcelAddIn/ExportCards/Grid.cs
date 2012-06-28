using System;
using System.Collections.Generic;

namespace TrelloExcelAddIn
{
	public class Grid
	{
		private readonly List<Cell> cells = new List<Cell>(); 

		 public void AddCell(int column, int row, string value, Type type)
		 {
		 	cells.Add(new Cell(column, row, value, type));
		 }

		public void RemoveCell(int column, int row)
		{
			cells.RemoveAll(c => c.Column == column && c.Row == row);
		}

		public IEnumerable<Cell> Cells { get { return cells; } }

		public class Cell
		{
			public int Column { get; private set; }
			public int Row { get; private set; }
			public string Value { get; private set; }
			public Type Type { get; set; }

			public Cell(int column, int row, string value, Type type)
			{
				Column = column;
				Row = row;
				Value = value;
				Type = type;
			}
		}
	}
}