using System.Threading.Tasks;
using Microsoft.Office.Core;
using TrelloNet;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;

namespace TrelloExcelAddIn
{
	public partial class ThisAddIn
	{
		public AddToTrelloPresenter Presenter { get; private set; }
		public CustomTaskPane TaskPane { get; private set; }
		public Trello Trello { get; private set; }
		public TaskScheduler TaskScheduler { get; private set; }

		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{
			Trello = new Trello("1ed8d91b5af35305a60e169a321ac248");
			var addToTrelloControl = new AddToTrelloControl();
			TaskPane = CustomTaskPanes.Add(addToTrelloControl, "Add to Trello");
			TaskPane.Width = 300;
			TaskPane.DockPositionRestrict = MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoHorizontal;
			TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
			Presenter = new AddToTrelloPresenter(addToTrelloControl, Trello, new SelectedRangeToCardsTransformer(), new ProcessImpl(), TaskScheduler);
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
