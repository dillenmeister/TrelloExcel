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
		private readonly TaskScheduler taskScheduler;
		private readonly IMessageBus messageBus;
		private readonly ITrello trello;
		private readonly ICreateNewCards transformer;
		private CancellationTokenSource exportCardsCancellationTokenSource;

		public ExportCardsPresenter(IExportCardsView view, ITrello trello, ICreateNewCards transformer, TaskScheduler taskScheduler, IMessageBus messageBus)
		{
			this.view = view;
			this.taskScheduler = taskScheduler;
			this.messageBus = messageBus;
			this.trello = trello;
			this.transformer = transformer;

			exportCardsCancellationTokenSource = new CancellationTokenSource();

			SetupMessageEventHandlers();
			SetupEventHandlers();
			SetupInitialState();
		}

		private void SetupMessageEventHandlers()
		{
			messageBus.Subscribe<TrelloWasAuthorizedEvent>(_ => FetchAndDisplayBoards());
		}

		private void SetupEventHandlers()
		{
			view.BoardWasSelected += BoardWasSelected;
			view.ExportCardsWasClicked += ExportCardsWasClicked;
			view.RefreshButtonWasClicked += RefreshButtonWasClicked;
			view.CancelButtonWasClicked += CancelButtonWasClicked;
		}

		private void SetupInitialState()
		{
			DisableStuff();
		}

		private void DisableStuff()
		{
			view.EnableSelectionOfBoards = false;
			view.EnableSelectionOfLists = false;
			view.EnableExportCards = false;
			view.EnableRefreshButton = false;
		}

		private void EnableStuff()
		{
			view.EnableSelectionOfBoards = true;
			view.EnableSelectionOfLists = true;
			view.EnableExportCards = true;
			view.EnableRefreshButton = true;
		}

		private void ExportCardsWasClicked(object sender, EventArgs eventArgs)
		{
			view.ShowStatusMessage("Adding cards...");
			view.HideCancelButton = false;
			view.HideExportButton = true;
			DisableStuff();

			var cards = transformer.CreateCards(view.SelectedList);
			var addCardsTask = Task.Factory.StartNew(() => ExportCards(cards), exportCardsCancellationTokenSource.Token);
			addCardsTask.ContinueWith(task =>
			{
				view.ShowStatusMessage(exportCardsCancellationTokenSource.IsCancellationRequested ? "Canceled!" : "All cards added!");
				exportCardsCancellationTokenSource = new CancellationTokenSource();

				EnableStuff();
				view.HideCancelButton = true;
				view.HideExportButton = false;
			}, taskScheduler);
		}

		private void RefreshButtonWasClicked(object sender, EventArgs eventArgs)
		{
			FetchAndDisplayBoards();
		}

		private void CancelButtonWasClicked(object sender, EventArgs eventArgs)
		{
			exportCardsCancellationTokenSource.Cancel();
		}

		private bool ExportCards(IEnumerable<NewCard> cards)
		{
			var totalCount = cards.Count();
			var currentCard = 0;
			foreach (var newCard in cards)
			{
				if (exportCardsCancellationTokenSource.Token.IsCancellationRequested)
					return false;

				Task.Factory.StartNew(() => view.ShowStatusMessage(@"Adding card {0}/{1}.", ++currentCard, totalCount),
					CancellationToken.None, TaskCreationOptions.None, taskScheduler);

				trello.Cards.Add(newCard);
			}

			return true;
		}

		private void FetchAndDisplayBoards()
		{
			Task.Factory.StartNew(() =>
			{
				// <WTF>

				var boards = trello.Boards.ForMe(BoardFilter.Open);

				var organizations = boards
					.Select(b => b.IdOrganization)
					.Where(s => !string.IsNullOrEmpty(s))
					.Distinct()
					.Select(orgId =>
					{
						try
						{
							return trello.Organizations.WithId(orgId);
						}
						catch (TrelloUnauthorizedException)
						{
							return null;
						}
					})
					.Where(o => o != null)
					.ToDictionary(organization => organization.Id);

				return boards.Select(b =>
				{
					var model = new BoardViewModel(b);
					if (b.IdOrganization != null && organizations.ContainsKey(b.IdOrganization))
						model.SetOrganizationName(organizations[b.IdOrganization].DisplayName);
					return model;
				});

				// </WTF>
			})
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
						EnableStuff();
					}
					else
					{
						HandleException(t.Exception);
					}
				}, taskScheduler);
		}

		private void HandleException(AggregateException exception)
		{
			view.EnableSelectionOfLists = false;
			view.EnableSelectionOfBoards = false;
			view.DisplayBoards(Enumerable.Empty<BoardViewModel>());
			view.DisplayLists(Enumerable.Empty<List>());

			if (exception.InnerException is TrelloUnauthorizedException)
				messageBus.Publish(new TrelloWasUnauthorizedEvent());

			view.ShowErrorMessage(exception.InnerException.Message);
		}
	}
}