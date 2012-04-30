﻿using System;
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
			AddCardsButton.Click += (sender, args) => AddCardsWasClicked(this, null);
			FetchBoardsButton.Click += (sender, args) => FetchBoardsWasClicked(this, null);
		}

		public event EventHandler BoardWasSelected;
		public event EventHandler AddCardsWasClicked;
		public event EventHandler FetchBoardsWasClicked;

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
