using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrelloNet;

namespace TrelloExcelAddIn
{
    public partial class ImportCardsControl : UserControl, IImportCardsView
    {
        public ImportCardsControl()
        {
            InitializeComponent();

            BoardComboBox.SelectedIndexChanged += (sender, args) => BoardWasSelected(this, null);
            ListsBox.ItemCheck += (sender, args) => BeginInvoke((MethodInvoker)(() => ListItemCheckedChanged(this, null)));
            ImportCardsButton.Click += (sender, args) => ImportCardsButtonWasClicked(this, null);
        }

        public event EventHandler BoardWasSelected;
        public event EventHandler ListItemCheckedChanged;
        public event EventHandler ImportCardsButtonWasClicked;

        public void DisplayBoards(IEnumerable<BoardViewModel> boards)
        {
            var boardViewModels = boards.ToList();

            BoardComboBox.DataSource = boardViewModels;

            if (!boardViewModels.Any())
                BoardComboBox.Text = "";
        }

        public bool EnableSelectionOfBoards
        {
            get { return BoardComboBox.Enabled; }
            set { BoardComboBox.Enabled = value; }
        }

        public IBoardId SelectedBoard
        {
            get { return (IBoardId)BoardComboBox.SelectedValue; }            
        }

        public bool EnableSelectionOfLists
        {
            get { return ListsBox.Enabled; }
            set { ListsBox.Enabled = value; }
        }

        public bool EnableImport
        {
            get { return ImportCardsButton.Enabled; }
            set { ImportCardsButton.Enabled = value; }
        }

        public IEnumerable<List> CheckedLists
        {
            get { return ListsBox.CheckedItems.Cast<List>(); }
        }

        public void DisplayLists(IEnumerable<List> lists)
        {
            ListsBox.DataSource = null;
            ListsBox.DataSource = lists;
        }
    }
}
