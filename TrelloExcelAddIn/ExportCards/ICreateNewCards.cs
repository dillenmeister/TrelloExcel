using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public interface ICreateNewCards
	{
		IEnumerable<NewCard> CreateCards(IListId list);
		IEnumerable<NewCard> CreateCards(Grid grid, IListId list);
	}
}