// ReSharper disable InconsistentNaming
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Machine.Specifications;
using TrelloExcelAddIn;
using TrelloNet;

namespace Tests.Specs
{
	[Subject(typeof(ExportCardsPresenter))]
	public class ExportCards : ExportCardsSpecs
	{
		public class when_trello_was_authorized
		{
			Establish context = () =>
			{
				boards = new List<Board>
				{
				    new Board { Name = "board 1" }, 
					new Board { Name = "board 2", IdOrganization = "123" }
				};

				A.CallTo(() => trello.Boards.ForMe(BoardFilter.Open)).Returns(boards);
				A.CallTo(() => trello.Organizations.WithId("123")).Returns(new Organization { Id = "123", DisplayName = "org 1" });
			};

			Because of = () =>
			{
				messageBus.Publish(new TrelloWasAuthorizedEvent(new Member()));
				Thread.Sleep(30);
			};

			It should_display_boards = () =>
				A.CallTo(() => view.DisplayBoards(A<IEnumerable<BoardViewModel>>._)).MustHaveHappened();

			It should_display_boards_with_correct_names = () =>
				A.CallTo(() => view.DisplayBoards(A<IEnumerable<BoardViewModel>>.That.Matches(b => b.ElementAt(0).ToString() == "board 1"))).MustHaveHappened();

			It should_display_boards_with_organization_name = () =>
				A.CallTo(() => view.DisplayBoards(A<IEnumerable<BoardViewModel>>.That.Matches(b => b.ElementAt(1).ToString() == "board 2 (org 1)"))).MustHaveHappened();

			static List<Board> boards;
		}

		public class when_a_board_is_selected
		{
			Establish context = () =>
			{
				var selectedBoard = new BoardId("dummy");

				A.CallTo(() => view.SelectedBoard).Returns(selectedBoard);
				A.CallTo(() => trello.Async.Lists.ForBoard(selectedBoard, ListFilter.Open))
					.Returns(Task.Factory.StartNew<IEnumerable<List>>(() => lists));
			};

			private Because of = () =>
			{
				view.BoardWasSelected += Raise.WithEmpty().Now;
				Thread.Sleep(30);
			};

			It should_display_lists_for_that_board = () =>
				A.CallTo(() => view.DisplayLists(lists)).MustHaveHappened();

			It should_enable_selection_of_lists = () =>
				view.EnableSelectionOfLists.ShouldBeTrue();

			It should_enable_export_cards = () =>
				view.EnableExportCards.ShouldBeTrue();

			static List<List> lists;
		}

		public class when_a_board_is_selected_and_trello_is_not_authenticated
		{
			Establish context = () =>
				A.CallTo(() => trello.Async.Lists.ForBoard(A<IBoardId>._, ListFilter.Open))
					.Returns(Task.Factory.StartNew<IEnumerable<List>>(() => { throw new TrelloUnauthorizedException("error"); }));

			Because of = () =>
			{
				view.BoardWasSelected += Raise.WithEmpty().Now;
				Thread.Sleep(30);
			};

			It should_show_an_error_message = () =>
				A.CallTo(() => view.ShowErrorMessage("error")).MustHaveHappened();

			It should_disable_selection_of_boards = () =>
				view.EnableSelectionOfBoards.ShouldBeFalse();

			It should_disable_selection_of_lists = () =>
				view.EnableSelectionOfLists.ShouldBeFalse();

			It should_empty_the_list_of_boards = () =>
				A.CallTo(() => view.DisplayBoards(A<IEnumerable<BoardViewModel>>.That.IsEmpty())).MustHaveHappened();

			It should_empty_the_list_of_lists = () =>
				A.CallTo(() => view.DisplayLists(A<IEnumerable<List>>.That.IsEmpty())).MustHaveHappened();
		}

		public class when_export_cards_is_clicked_and_two_rows_are_selected
		{
			Establish context = () =>
				A.CallTo(() => transformer.CreateCards(A<IListId>._)).Returns(newCards);

			Because of = () =>
			{
				view.ExportCardsWasClicked += Raise.WithEmpty().Now;
				Thread.Sleep(30);
			};

			It should_display_a_status_message = () =>
				A.CallTo(() => view.ShowStatusMessage("Adding cards...", A<object[]>._)).MustHaveHappened();

			It should_add_each_card_to_trello = () =>
			{
				A.CallTo(() => trello.Cards.Add(A<NewCard>.That.Matches(c => c.Name == "card 1"))).MustHaveHappened();
				A.CallTo(() => trello.Cards.Add(A<NewCard>.That.Matches(c => c.Name == "card 2"))).MustHaveHappened();
			};

			It should_display_a_status_message_for_each_card_added = () =>
				A.CallTo(() => view.ShowStatusMessage("Adding card {0}/{1}.", A<object[]>._)).MustHaveHappened(Repeated.Exactly.Twice);

			It should_display_a_status_message_when_all_cards_are_added = () =>
				A.CallTo(() => view.ShowStatusMessage("All cards added!", A<object[]>._)).MustHaveHappened();

			static CardInfo[] newCards = new[] { new CardInfo { Name = "card 1", ListId = new ListId("dummy") }, new CardInfo { Name = "card 2", ListId = new ListId("dummy") } };
		}

		public class when_the_refresh_button_is_clicked
		{
			Establish context = () =>
			{
				A.CallTo(() => trello.Boards.ForMe(BoardFilter.Open)).Returns(new[] { new Board { Name = "board 1" } });
			};

			Because of = () =>
			{
				view.RefreshButtonWasClicked += Raise.WithEmpty().Now;
				Thread.Sleep(30);
			};

			It should_refresh_the_list_of_boards = () =>
				A.CallTo(() => view.DisplayBoards(A<IEnumerable<BoardViewModel>>.That.Matches(b => b.First().ToString() == "board 1"))).MustHaveHappened();

			static List<Board> boards;
		}
	}

	public abstract class ExportCardsSpecs
	{
		protected static ExportCardsPresenter presenter;
		protected static IExportCardsView view;
		protected static ICreateNewCards transformer;
		protected static ITrello trello;
		protected static IMessageBus messageBus;

		Establish context = () =>
		{
			view = A.Fake<IExportCardsView>();
			transformer = A.Fake<ICreateNewCards>();
			messageBus = new MessageBus();
			trello = A.Fake<ITrello>();
			presenter = new ExportCardsPresenter(view, trello, transformer, TaskScheduler.Current, messageBus);
		};
	}
}
// ReSharper restore InconsistentNaming