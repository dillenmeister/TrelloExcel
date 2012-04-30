using System.Threading.Tasks;
using Microsoft.Office.Core;
using TrelloNet;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;

namespace TrelloExcelAddIn
{
	public partial class ThisAddIn
	{
		public Trello Trello { get; private set; }		

		public ExportCardsPresenter ExportCardsPresenter { get; private set; }
		public AuthorizePresenter AuthorizePresenter { get; set; }

		public CustomTaskPane ExportCardsTaskPane { get; private set; }
		public TaskScheduler TaskScheduler { get; private set; }

		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{						
			Trello = new Trello("1ed8d91b5af35305a60e169a321ac248");

			var exportCardsControl = new ExportCardsControl();
			var authorizeForm = new AuthorizationDialog();
			var messageBus = new MessageBus();

			ExportCardsTaskPane = CustomTaskPanes.Add(exportCardsControl, "Add to Trello");
			ExportCardsTaskPane.Width = 300;
			ExportCardsTaskPane.DockPositionRestrict = MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;

			TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

			ExportCardsPresenter = new ExportCardsPresenter(exportCardsControl, Trello, new SelectedRangeToCardsTransformer(), new ProcessImpl(), TaskScheduler, messageBus);
			AuthorizePresenter = new AuthorizePresenter(authorizeForm, Trello, messageBus);
			Globals.Ribbons.TrelloRibbon.SetMessageBus(messageBus);
		}

		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{
		}

		#region VSTO generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(ThisAddIn_Startup);
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}

		#endregion
	}
}
