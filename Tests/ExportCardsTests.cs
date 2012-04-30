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
	}
}