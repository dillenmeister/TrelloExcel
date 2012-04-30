using Microsoft.Office.Tools.Ribbon;
using TrelloExcelAddIn.Properties;

namespace TrelloExcelAddIn
{
	public partial class TrelloRibbon
	{
		private void TrelloRibbon_Load(object sender, RibbonUIEventArgs e)
		{

		}

		private void AddToTrelloButton_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ExportCardsTaskPane.Visible = !Globals.ThisAddIn.ExportCardsTaskPane.Visible;
		}

		private void AuthorizeButton_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.AuthorizePresenter.StartAuthorization();
		}

		public void SetMessageBus(IMessageBus messageBus)
		{
			messageBus.Subscribe<TrelloWasAuthorizedEvent>(@event =>
			{
				ExportCardsButton.Enabled = true;
				AuthorizeButton.Image = Resources._112_Tick_Green_32x32_72;
			});
		}
	}
}
