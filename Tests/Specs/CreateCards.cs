using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using TrelloExcelAddIn;
using TrelloNet;

namespace Tests.Specs
{
	[Subject(typeof(GridToNewCardTransformer))]
	public class CreateCards : CreateCardsSpec
	{
		public class when_grid_has_no_cells
		{
			Because of = () =>
				CreateNewCards();

			It should_create_no_cards = () =>
				createdCards.ShouldBeEmpty();
		}

		public class when_grid_has_one_cell
		{
			Because of = () =>
			{
				grid.AddCell(1, 1, "cell 1,1");
				CreateNewCards();
			};

			It should_create_one_card = () =>
				createdCards.Count().ShouldEqual(1);

			It should_set_the_name_of_the_card_to_the_value_of_the_cell = () =>
				createdCards.First().Name.ShouldEqual("cell 1,1");

			It should_leave_the_description_empty = () =>
				createdCards.First().Desc.ShouldBeNull();

			It should_leave_the_due_date_empty = () =>
				createdCards.First().Due.ShouldBeNull();

		}

		public class when_grid_has_two_cells_in_the_same_column
		{
			Because of = () =>
			{
				grid.AddCell(1, 1, "cell 1,1");
				grid.AddCell(1, 2, "cell 1,2");			
				CreateNewCards();
			};

			It should_create_two_cards = () =>
				createdCards.Count().ShouldEqual(2);

			It should_set_the_name_of_the_first_card_to_the_value_of_the_first_cell = () =>
				createdCards.ElementAt(0).Name.ShouldEqual("cell 1,1");

			It should_set_the_name_of_the_second_card_to_the_value_of_the_second_cell = () =>
				createdCards.ElementAt(1).Name.ShouldEqual("cell 1,2");
		}

		public class when_grid_has_two_cells_on_the_same_row
		{
			Because of = () =>
			{
				grid.AddCell(1, 1, "cell 1,1");
				grid.AddCell(2, 1, "cell 2,1");
				CreateNewCards();
			};

			It should_create_one_card = () =>
				createdCards.Count().ShouldEqual(1);

			It should_set_the_name_of_the_card_to_the_value_of_the_first_cell = () =>
				createdCards.First().Name.ShouldEqual("cell 1,1");

			It should_set_the_description_of_the_card_to_the_value_of_the_second_cell = () =>
				createdCards.First().Desc.ShouldEqual("cell 2,1");
		}

		public class when_grid_has_two_cells_on_the_same_column_with_one_column_apart
		{
			Because of = () =>
			{
				grid.AddCell(1, 1, "cell 1,1");
				grid.AddCell(3, 1, "cell 3,1");
				CreateNewCards();
			};

			It should_create_one_card = () =>
				createdCards.Count().ShouldEqual(1);

			It should_set_the_name_of_the_card_to_the_value_of_the_first_cell = () =>
				createdCards.First().Name.ShouldEqual("cell 1,1");

			It should_set_the_description_of_the_card_to_the_value_of_the_cell_in_the_third_column = () =>
				createdCards.First().Desc.ShouldEqual("cell 3,1");
		}

		public class when_grid_has_two_cells_on_different_row_and_column
		{
			Because of = () =>
			{
				grid.AddCell(1, 1, "cell 1,1");
				grid.AddCell(2, 2, "cell 2,2");	
				CreateNewCards();
			};

			It should_create_one_card = () =>
				createdCards.Count().ShouldEqual(1);

			It should__only_create_a_card_for_the_leftmost_colum = () =>
				createdCards.First().Name.ShouldEqual("cell 1,1");
		}

		public class when_any_card_is_created
		{
			Because of = () =>
			{
				listId = "listId";
				grid.AddCell(1, 1, "any card");
				CreateNewCards();
			};

			It should_set_the_list_for_the_card = () =>
				createdCards.First().ListId.GetListId().ShouldEqual(listId);
		}
	}

	public class CreateCardsSpec
	{
		protected static GridToNewCardTransformer transformer;
		protected static Grid grid;
		protected static string listId;
		protected static IEnumerable<CardInfo> createdCards;

		Establish context = () =>
		{
			grid = new Grid();
			listId = "X";
		};

		protected static void CreateNewCards()
		{
			transformer = new GridToNewCardTransformer();
			createdCards = transformer.CreateCards(grid, new ListId(listId));
		}
	}
}