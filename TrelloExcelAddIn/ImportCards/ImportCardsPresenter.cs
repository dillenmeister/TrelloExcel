using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using TrelloNet;

namespace TrelloExcelAddIn
{
    public class ImportCardsPresenter
    {
        private readonly IImportCardsView view;
        private readonly IMessageBus messageBus;
        private readonly TrelloHelper trelloHelper;
        private readonly ITrello trello;
        private readonly TaskScheduler taskScheduler;

        public ImportCardsPresenter(IImportCardsView view, IMessageBus messageBus, ITrello trello, TaskScheduler taskScheduler)
        {
            this.view = view;
            this.messageBus = messageBus;
            this.trello = trello;
            this.taskScheduler = taskScheduler;
            trelloHelper = new TrelloHelper(trello);

            SetupMessageEventHandlers();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            view.BoardWasSelected += BoardWasSelected;
            view.ListItemCheckedChanged += ListItemCheckedChanged;
            view.ImportCardsButtonWasClicked += ImportCardsButtonWasClicked;
        }

        private void ImportCardsButtonWasClicked(object sender, EventArgs eventArgs)
        {
            trello.Async.Cards.ForBoard(view.SelectedBoard, BoardCardFilter.Open)
                .ContinueWith(t =>
                {
                    var cards = t.Result.Where(c => view.CheckedLists.Select(cl => cl.Id).Contains(c.IdList)).ToList();
                    var rangeThatFitsAllCards = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection.Resize[cards.Count(), 3];
                    var address = rangeThatFitsAllCards.AddressLocal;                    
                    rangeThatFitsAllCards.Copy();
                    Globals.ThisAddIn.Application.CutCopyMode = XlCutCopyMode.xlCopy;     
                    rangeThatFitsAllCards.Insert(XlInsertShiftDirection.xlShiftDown);
                    var before = Globals.ThisAddIn.Application.ActiveSheet.Range(address).Resize[cards.Count(), 3];
                    
                    before.Value2 = cards.Select(c => new[] { c.Name, c.Desc, null }).ToArray().ToMultidimensionalArray();
                    before.Select();
                    before.Columns.AutoFit();
                });
        }      

        private void ListItemCheckedChanged(object sender, EventArgs eventArgs)
        {
            view.EnableImport = view.CheckedLists.Any();
        }

        private void BoardWasSelected(object sender, EventArgs e)
        {
            trello.Async.Lists.ForBoard(view.SelectedBoard)
                .ContinueWith(t =>
                {
                    if (t.Exception == null)
                    {
                        view.DisplayLists(t.Result);
                        view.EnableSelectionOfLists = true;
                    }
                    else
                    {
                        HandleException(t.Exception);
                    }
                }, taskScheduler);
        }

        private void SetupMessageEventHandlers()
        {
            messageBus.Subscribe<TrelloWasAuthorizedEvent>(_ => FetchAndDisplayBoards());
        }

        private void FetchAndDisplayBoards()
        {
            Task.Factory.StartNew(() => trelloHelper.FetchBoardViewModelsForMe())
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

        private void HandleException(AggregateException exception)
        {
        }
    }

    public static class LinqExtensions
    {
        public static T[,] ToMultidimensionalArray<T>(this T[][] jaggedArray)
        {
            int rows = jaggedArray.Length;
            int cols = jaggedArray.Max(subArray => subArray.Length);
            T[,] array = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }
            return array;
        }      
    }
}