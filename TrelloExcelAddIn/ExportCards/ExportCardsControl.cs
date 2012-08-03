using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public partial class ExportCardsControl : UserControl, IExportCardsView
	{
		public ExportCardsControl()
		{
			InitializeComponent();

			BoardComboBox.SelectedIndexChanged += (sender, args) => BoardWasSelected(this, null);
			AddCardsButton.Click += (sender, args) => ExportCardsWasClicked(this, null);
			RefreshButton.Click += (sender, args) => RefreshButtonWasClicked(this, null);
			CancelExportButton.Click += (sender, args) => CancelButtonWasClicked(this, null);
		}


		public event EventHandler BoardWasSelected;
		public event EventHandler ExportCardsWasClicked;
		public event EventHandler RefreshButtonWasClicked;
		public event EventHandler CancelButtonWasClicked;

		public bool EnableSelectionOfBoards
		{
			get { return BoardComboBox.Enabled; }
			set { BoardComboBox.Enabled = value; }
		}

		public bool EnableSelectionOfLists
		{
			get { return ListComboBox.Enabled; }
			set { ListComboBox.Enabled = value; }
		}

		public bool EnableExportCards
		{
			get { return AddCardsButton.Enabled; }
			set { AddCardsButton.Enabled = value; }
		}

		public bool EnableRefreshButton
		{
			get { return RefreshButton.Enabled; }
			set { RefreshButton.Enabled = value; }
		}

		public bool HideCancelButton
		{
			get { return !CancelExportButton.Visible; }
			set { CancelExportButton.Visible = !value; }
		}

		public bool HideExportButton
		{
			get { return !AddCardsButton.Visible; }
			set { AddCardsButton.Visible = !value; }
		}

		public IBoardId SelectedBoard
		{
			get { return (IBoardId)BoardComboBox.SelectedValue; }
		}

		public IListId SelectedList
		{
			get { return (IListId)ListComboBox.SelectedValue; }
		}

		public void DisplayBoards(IEnumerable<BoardViewModel> boards)
		{
			var boardViewModels = boards.ToList();

			BoardComboBox.DataSource = boardViewModels;

			if (!boardViewModels.Any())
				BoardComboBox.Text = "";
		}

		public void DisplayLists(IEnumerable<List> lists)
		{
			ListComboBox.DataSource = lists;

			if (!lists.Any())
				ListComboBox.Text = "";
		}

		public void ShowStatusMessage(string text, params object[] args)
		{
			StatusLabel.Text = string.Format(text, args);
		}

		public void ShowErrorMessage(string message)
		{
			MessageBox.Show(message, "Message");
		}
	}
}
