using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public interface ICreateNewCards
	{
		IEnumerable<CardInfo> CreateCards(IListId list);
		IEnumerable<CardInfo> CreateCards(Grid grid, IListId list);
	}
}