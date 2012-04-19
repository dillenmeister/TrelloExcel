using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using TrelloExcelAddIn;
using TrelloNet;
using List = TrelloNet.List;

namespace Tests
{
	[TestFixture]
	public class ExportCardsTests
	{
		private IExportCardsView view;
		private IProcess process;
		private ITrello trello;
		private ISelectedRangeToCardsTransformer transformer;
		private ExportCardsPresenter presenter;

		[SetUp]
		public void Setup()
		{
			view = A.Fake<IExportCardsView>();
			process = A.Fake<IProcess>();
			trello = A.Fake<ITrello>();
			transformer = A.Fake<ISelectedRangeToCardsTransformer>();

			presenter = new ExportCardsPresenter(view, trello, transformer, process, TaskScheduler.Current);
		}

		[TestCase("http://www.someurl.com/")]
		[TestCase("http://www.someotherurl.com/")]
		public void AuthenticationUrlWasClicked_Always_OpensBrowserWithTrelloAuthenticationUrlIdentifyingAsTrelloExcel(string url)
		{
			A.CallTo(() => trello.GetAuthorizationUrl("TrelloExcel", A<Scope>._, A<Expiration>._)).Returns(new Uri(url));

			view.AuthorizationUrlWasClicked += Raise.WithEmpty().Now;

			A.CallTo(() => process.Start(url)).MustHaveHappened();
		}

		[TestCase("http://www.someurl.com/")]
		[TestCase("http://www.someotherurl.com/")]
		public void AuthenticationUrlWasClicked_Always_OpensBrowserWithTrelloAuthenticationUrlWithReadWriteScope(string url)
		{
			A.CallTo(() => trello.GetAuthorizationUrl(A<string>._, Scope.ReadWrite, A<Expiration>._)).Returns(new Uri(url));

			view.AuthorizationUrlWasClicked += Raise.WithEmpty().Now;

			A.CallTo(() => process.Start(url)).MustHaveHappened();
		}

		[TestCase("http://www.someurl.com/")]
		[TestCase("http://www.someotherurl.com/")]
		public void AuthenticationUrlWasClicked_Always_OpensBrowserWithTrelloAuthenticationUrlWithExpirationOfOneHour(string url)
		{
			A.CallTo(() => trello.GetAuthorizationUrl(A<string>._, A<Scope>._, Expiration.OneHour)).Returns(new Uri(url));

			view.AuthorizationUrlWasClicked += Raise.WithEmpty().Now;

			A.CallTo(() => process.Start(url)).MustHaveHappened();
		}

		[TestCase("token1")]
		[TestCase("token2")]
		public void AuthorizationTokenWasConfirmed_Always_AuthorizesTrello(string token)
		{
			A.CallTo(() => view.AuthorizationToken).Returns(token);

			view.AuthorizationTokenWasConfirmed += Raise.WithEmpty().Now;

			A.CallTo(() => trello.Authorize(token)).MustHaveHappened();
		}

		[Test]
		public void AuthorizationTokenWasConfirmed_NoException_DisplaysOpenBoards()
		{
			var boards = new List<Board>();
			var task = Task.Factory.StartNew<IEnumerable<Board>>(() => boards);

			A.CallTo(() => trello.Async.Boards.ForMe(BoardFilter.Open)).Returns(task);

			view.AuthorizationTokenWasConfirmed += Raise.WithEmpty().Now;
			Thread.Sleep(10);

			A.CallTo(() => view.DisplayBoards(boards)).MustHaveHappened();
		}

		[Test]
		public void AuthorizationTokenWasConfirmed_NoException_EnablesSelectionOfBoards()
		{
			var task = Task.Factory.StartNew<IEnumerable<Board>>(() => new List<Board>());

			A.CallTo(() => trello.Async.Boards.ForMe(BoardFilter.Open)).Returns(task);

			view.AuthorizationTokenWasConfirmed += Raise.WithEmpty().Now;
			Thread.Sleep(10);

			Assert.That(view.EnableSelectionOfBoards, Is.True);
		}

		[Test]
		public void AuthorizationTokenWasConfirmed_ExceptionWasThrown_ShowErrorMessage()
		{
			var task = Task.Factory.StartNew<IEnumerable<Board>>(
				() => { throw new TrelloUnauthorizedException("unauthorized"); });

			A.CallTo(() => trello.Async.Boards.ForMe(BoardFilter.Open)).Returns(task);

			view.AuthorizationTokenWasConfirmed += Raise.WithEmpty().Now;
			Thread.Sleep(10);

			A.CallTo(() => view.ShowErrorMessage("unauthorized")).MustHaveHappened();
		}

		[Test]
		public void AuthorizationTokenWasConfirmed_ExceptionWasThrown_DisablesSelectionOfBoards()
		{
			var task = Task.Factory.StartNew<IEnumerable<Board>>(
				() => { throw new TrelloUnauthorizedException("unauthorized"); });

			A.CallTo(() => trello.Async.Boards.ForMe(BoardFilter.Open)).Returns(task);

			view.AuthorizationTokenWasConfirmed += Raise.WithEmpty().Now;
			Thread.Sleep(10);

			Assert.That(view.EnableSelectionOfBoards, Is.False);
		}

		[Test]
		public void AuthorizationTokenWasConfirmed_ExceptionWasThrown_DisplaysNoBoards()
		{
			var task = Task.Factory.StartNew<IEnumerable<Board>>(
				() => { throw new TrelloUnauthorizedException("unauthorized"); });

			A.CallTo(() => trello.Async.Boards.ForMe(BoardFilter.Open)).Returns(task);

			view.AuthorizationTokenWasConfirmed += Raise.WithEmpty().Now;
			Thread.Sleep(10);

			A.CallTo(() => view.DisplayBoards(A<IEnumerable<Board>>.That.IsEmpty())).MustHaveHappened();			
		}

		[Test]
		public void BoardWasSelected_NoException_DisplayListsForThatBoard()
		{
			var selectedBoard = new BoardId("dummy");
			A.CallTo(() => view.SelectedBoard).Returns(selectedBoard);

			var lists = new List<List>();
			A.CallTo(() => trello.Async.Lists.ForBoard(selectedBoard, ListFilter.Open))
				.Returns(Task.Factory.StartNew<IEnumerable<List>>(() => lists));

			view.BoardWasSelected += Raise.WithEmpty().Now;
			Thread.Sleep(10);

			A.CallTo(() => view.DisplayLists(lists)).MustHaveHappened();
		}

		[Test]
		public void BoardWasSelected_NoException_EnableListsSelection()
		{
			var selectedBoard = new BoardId("dummy");
			A.CallTo(() => view.SelectedBoard).Returns(selectedBoard);

			var lists = new List<List>();
			A.CallTo(() => trello.Async.Lists.ForBoard(selectedBoard, ListFilter.Open))
				.Returns(Task.Factory.StartNew<IEnumerable<List>>(() => lists));

			view.BoardWasSelected += Raise.WithEmpty().Now;
			Thread.Sleep(30);

			Assert.That(view.EnableSelectionOfLists, Is.True);			
		}

		[Test]
		public void AuthorizationTokenWasChanged_TokenIsEmpty_DisableAbilityToAuthorize()
		{
			A.CallTo(() => view.AuthorizationToken).Returns("");

			view.AuthorizationTokenWasChanged += Raise.WithEmpty().Now;

			Assert.That(view.EnableAuthorize, Is.False);
		}

		[Test]
		public void AuthorizationTokenWasChanged_TokenIsNotEmpty_EnableAbilityToAuthorize()
		{
			A.CallTo(() => view.AuthorizationToken).Returns("dummy");

			view.AuthorizationTokenWasChanged += Raise.WithEmpty().Now;

			Assert.That(view.EnableAuthorize, Is.True);
		}
	}
}