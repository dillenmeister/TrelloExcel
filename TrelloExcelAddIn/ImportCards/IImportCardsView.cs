using System;
using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
    public interface IImportCardsView
    {
        event EventHandler BoardWasSelected;

        void DisplayBoards(IEnumerable<BoardViewModel> boards);
        bool EnableSelectionOfBoards { get; set; }
        IBoardId SelectedBoard { get; }
        bool EnableSelectionOfLists { get; set; }
        bool EnableImport { get; set; }
        IEnumerable<List> CheckedLists { get; }
        void DisplayLists(IEnumerable<List> lists);
        event EventHandler ListItemCheckedChanged;
        event EventHandler ImportCardsButtonWasClicked;
        void ShowStatusMessage(string message);
        void ShowErrorMessage(string message);
    }
}