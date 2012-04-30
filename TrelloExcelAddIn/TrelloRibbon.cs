using Microsoft.Office.Tools.Ribbon;

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
			messageBus.Subscribe<TrelloWasUnauthorizedEvent>(@event =>
			{
				ExportCardsButton.Enabled = false;
				AuthorizeButton.Enabled = true;
				AuthorizeButton.Label = "Login";
			});

			messageBus.Subscribe<TrelloWasAuthorizedEvent>(@event =>
			{
				ExportCardsButton.Enabled = true;
				AuthorizeButton.Enabled = false;
				AuthorizeButton.Label = string.Format("Logged in as {0}", @event.Member.Username);
			});
		}
	}
}
