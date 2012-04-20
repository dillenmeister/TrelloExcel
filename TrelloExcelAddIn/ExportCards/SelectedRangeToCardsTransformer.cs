using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using TrelloNet;

namespace TrelloExcelAddIn
{
	internal class SelectedRangeToCardsTransformer : ISelectedRangeToCardsTransformer
	{
		public IEnumerable<NewCard> GetCards(IListId listId)
		{
			var range = GetSelectedRange();

			var cardsToAdd = new List<NewCard>();
			for (var row = 1; row <= range.Count; row++)
			{
				var cardName = GetCardName(row, range);

				if (!string.IsNullOrEmpty(cardName))
				{
					var cardDescription = GetCardDescription(row, range);
					cardsToAdd.Add(new NewCard(cardName, listId) { Desc = cardDescription });
				}
			}

			return cardsToAdd;
		}

		private static string GetCardName(int row, Range range)
		{
			return Convert.ToString(range[row, 1].Value);
		}

		private static string GetCardDescription(int row, Range range)
		{
			if (range.Columns.Count > 1)
				return Convert.ToString(range[row, 2].Value);
			if (range.Areas.Count > 1)
				return Convert.ToString(range.Areas[2][row, 1].Value);

			return null;
		}

		private static Range GetSelectedRange()
		{
			return Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;
		}
	}
}