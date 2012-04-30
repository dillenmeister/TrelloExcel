using System;
using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public interface IExportCardsView
	{
		event EventHandler BoardWasSelected;
		event EventHandler AddCardsWasClicked;
		event EventHandler FetchBoardsWasClicked;
		
		bool EnableSelectionOfBoards { get; set; }
		bool EnableSelectionOfLists { get; set; }
		bool EnableAddCards { get; set; }
		IBoardId SelectedBoard { get; }
		IListId SelectedList { get; }
		void DisplayBoards(IEnumerable<Board> boards);
		void DisplayLists(IEnumerable<List> lists);
		void ShowErrorMessage(string message);
		void ShowStatusMessage(string message, params object[] args);
	}
}