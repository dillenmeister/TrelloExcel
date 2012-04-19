using System;
using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public interface IExportCardsView
	{
		event EventHandler AuthorizationUrlWasClicked;
		event EventHandler AuthorizationTokenWasConfirmed;
		event EventHandler AuthorizationTokenWasChanged;
		event EventHandler BoardWasSelected;
		event EventHandler AddCardsWasClicked;

		string AuthorizationToken { get; }
		bool EnableSelectionOfBoards { get; set; }
		bool EnableSelectionOfLists { get; set; }
		bool EnableAuthorize { get; set; }
		bool EnableAddCards { get; set; }
		IBoardId SelectedBoard { get; }
		IListId SelectedList { get; }
		void DisplayBoards(IEnumerable<Board> boards);
		void DisplayLists(IEnumerable<List> lists);
		void ShowErrorMessage(string message);
		void ShowStatusMessage(string message, params object[] args);
	}
}