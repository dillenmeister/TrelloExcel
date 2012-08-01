using System.Threading.Tasks;
using FakeItEasy;
using Machine.Specifications;
using TrelloExcelAddIn;
using TrelloNet;

namespace Tests.Specs
{
    [Subject(typeof(ImportCardsPresenter))]
    public class ImportCards : ImportCardsSpecs
    {       
    }

    public abstract class ImportCardsSpecs
    {
        protected static ImportCardsPresenter presenter;
        protected static IImportCardsView view;
        protected static IMessageBus messageBus;
        protected static ITrello trello;

        Establish context = () =>
        {
            view = A.Fake<IImportCardsView>();
            messageBus = A.Fake<IMessageBus>();
            trello = A.Fake<ITrello>();
            presenter = new ImportCardsPresenter(view, messageBus, trello, TaskScheduler.Current);
        };
    }
}