using System;
using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public interface IExportCardsView
	{
		event EventHandler BoardWasSelected;
		event EventHandler ExportCardsWasClicked;
		event EventHandler RefreshButtonWasClicked;
		event EventHandler CancelButtonWasClicked;

		bool EnableSelectionOfBoards { get; set; }
		bool EnableSelectionOfLists { get; set; }
		bool EnableExportCards { get; set; }
		bool EnableRefreshButton { get; set; }
		bool HideCancelButton { get; set; }
		bool HideExportButton { get; set; }
		IBoardId SelectedBoard { get; }
		IListId SelectedList { get; }
		void DisplayBoards(IEnumerable<BoardViewModel> boards);
		void DisplayLists(IEnumerable<List> lists);
		void ShowErrorMessage(string message);
		void ShowStatusMessage(string message, params object[] args);		
	}
}