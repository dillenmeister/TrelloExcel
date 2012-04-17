using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public partial class AddToTrelloControl : UserControl, IAddToTrelloView
	{
		public AddToTrelloControl()
		{
			InitializeComponent();

			AuthenticationUrl.LinkClicked += (s, a) => AuthorizationUrlWasClicked(this, null);
			AuthenticateButton.Click += (s, a) => AuthorizationTokenWasConfirmed(this, null);
			BoardComboBox.SelectedIndexChanged += (sender, args) => BoardWasSelected(this, null);
			TokenTextBox.TextChanged += (sender, args) => AuthorizationTokenWasChanged(this, null);
			AddCardsButton.Click += (sender, args) => AddCardsWasClicked(this, null);
		}

		public event EventHandler AuthorizationUrlWasClicked;
		public event EventHandler AuthorizationTokenWasConfirmed;
		public event EventHandler AuthorizationTokenWasChanged;
		public event EventHandler BoardWasSelected;
		public event EventHandler AddCardsWasClicked;

		public string AuthorizationToken
		{
			get { return TokenTextBox.Text; }
		}

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

		public bool EnableAuthorize
		{
			get { return AuthenticateButton.Enabled; }
			set { AuthenticateButton.Enabled = value; }
		}

		public bool EnableAddCards
		{
			get { return AddCardsButton.Enabled; }
			set { AddCardsButton.Enabled = value; }
		}

		public IBoardId SelectedBoard
		{
			get { return (IBoardId)BoardComboBox.SelectedValue; }
		}

		public IListId SelectedList
		{
			get { return (IListId)ListComboBox.SelectedValue; }
		}

		public void DisplayBoards(IEnumerable<Board> boards)
		{
			BoardComboBox.DataSource = boards;

			if (!boards.Any())
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
			MessageBox.Show(message);
		}
	}
}
