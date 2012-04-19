using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public class ExportCardsPresenter
	{
		private readonly IExportCardsView view;
		private readonly IProcess process;
		private readonly TaskScheduler taskScheduler;
		private readonly ITrello trello;
		private readonly ISelectedRangeToCardsTransformer transformer;

		public ExportCardsPresenter(IExportCardsView view, ITrello trello, ISelectedRangeToCardsTransformer transformer, IProcess process, TaskScheduler taskScheduler)
		{
			this.view = view;
			this.process = process;
			this.taskScheduler = taskScheduler;
			this.trello = trello;
			this.transformer = transformer;

			SetupEventHandlers();
			SetupInitialState();
		}

		private void SetupEventHandlers()
		{
			view.AuthorizationUrlWasClicked += AuthorizationUrlWasClicked;
			view.AuthorizationTokenWasConfirmed += AuthorizationTokenWasConfirmed;
			view.BoardWasSelected += BoardWasSelected;
			view.AuthorizationTokenWasChanged += AuthorizationTokenWasChanged;
			view.AddCardsWasClicked += AddCardsWasClicked;
		}

		private void SetupInitialState()
		{
			view.EnableAuthorize = false;
			view.EnableSelectionOfBoards = false;
			view.EnableSelectionOfLists = false;
			view.EnableAddCards = false;
		}

		private void AddCardsWasClicked(object sender, EventArgs eventArgs)
		{
			view.ShowStatusMessage("Adding cards...");

			var cards = transformer.GetCards(view.SelectedList);
			var addCardsTask = Task.Factory.StartNew(() => AddCards(cards));
			addCardsTask.ContinueWith(t => view.ShowStatusMessage("All cards added!"), taskScheduler);
		}

		private void AddCards(IEnumerable<NewCard> cards)
		{
			var totalCount = cards.Count();
			var currentCard = 0;
			foreach (var newCard in cards)
			{
				Task.Factory.StartNew(() => view.ShowStatusMessage(@"Adding card {0}/{1}.", ++currentCard, totalCount),
					CancellationToken.None, TaskCreationOptions.None, taskScheduler);

				trello.Cards.Add(newCard);
			}
		}

		private void AuthorizationTokenWasChanged(object sender, EventArgs eventArgs)
		{
			view.EnableAuthorize = view.AuthorizationToken.Length > 0;
		}

		private void AuthorizationTokenWasConfirmed(object sender, EventArgs e)
		{
			trello.Authorize(view.AuthorizationToken);
			FetchAndDisplayBoards();
		}

		private void FetchAndDisplayBoards()
		{
			trello.Async.Boards.ForMe(BoardFilter.Open)
				.ContinueWith(t =>
				{
					if (t.Exception == null)
					{
						view.DisplayBoards(t.Result);
						view.EnableSelectionOfBoards = true;
					}
					else
					{
						HandleException(t.Exception);
					}
				}, taskScheduler);
		}

		private void BoardWasSelected(object sender, EventArgs e)
		{
			FetchAndDisplayLists();
		}

		private void FetchAndDisplayLists()
		{
			trello.Async.Lists.ForBoard(view.SelectedBoard)
				.ContinueWith(t =>
				{
					if (t.Exception == null)
					{
						view.DisplayLists(t.Result);
						view.EnableSelectionOfLists = true;
						view.EnableAddCards = true;
					}
					else
					{
						HandleException(t.Exception);
					}
				}, taskScheduler);
		}

		private void AuthorizationUrlWasClicked(object sender, EventArgs e)
		{
			var url = trello.GetAuthorizationUrl("TrelloExcel", Scope.ReadWrite, Expiration.OneHour);
			process.Start(url.ToString());
		}

		private void HandleException(AggregateException exception)
		{
			view.EnableSelectionOfLists = false;
			view.EnableSelectionOfBoards = false;
			view.DisplayBoards(Enumerable.Empty<Board>());
			view.DisplayLists(Enumerable.Empty<List>());

			view.ShowErrorMessage(exception.InnerException.Message);
		}
	}
}