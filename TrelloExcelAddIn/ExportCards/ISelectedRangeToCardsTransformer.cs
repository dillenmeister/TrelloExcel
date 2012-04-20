using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public interface ISelectedRangeToCardsTransformer
	{
		IEnumerable<NewCard> GetCards(IListId listId);
	}
}