using System.Collections.Generic;
using Microsoft.Office.Tools.Ribbon;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public partial class TrelloRibbon
	{
		private static readonly Dictionary<string, Expiration> ButtonIdsToExpirationMap = new Dictionary<string, Expiration>
		{
			{ "LoginExpireOneHourButton", Expiration.OneHour }, 
			{ "LoginExpireOneDayButton", Expiration.OneDay }, 
			{ "LoginExpire30DaysButton", Expiration.ThirtyDays }, 
			{ "LoginNeverExpireButton", Expiration.Never }
		};

		private void TrelloRibbon_Load(object sender, RibbonUIEventArgs e)
		{
		}

		private void AddToTrelloButton_Click(object sender, RibbonControlEventArgs e)
		{
			Globals.ThisAddIn.ExportCardsTaskPane.Visible = !Globals.ThisAddIn.ExportCardsTaskPane.Visible;
		}

		public void SetMessageBus(IMessageBus messageBus)
		{
			messageBus.Subscribe<TrelloWasUnauthorizedEvent>(@event =>
			{
				ExportCardsButton.Enabled = false;
				LoginSplitButton.Visible = true;
				LoggedInButton.Visible = false;
			});

			messageBus.Subscribe<TrelloWasAuthorizedEvent>(@event =>
			{
				ExportCardsButton.Enabled = true;
				LoginSplitButton.Visible = false;
				LoggedInButton.Label = string.Format("Logged in as {0}", @event.Member.Username);
				LoggedInButton.Visible = true;
			});
		}

		private void LoginButton_Click(object sender, RibbonControlEventArgs e)
		{
			Expiration expiration;
			if(!ButtonIdsToExpirationMap.TryGetValue(e.Control.Id, out expiration))
				expiration = Expiration.Never;
			Globals.ThisAddIn.AuthorizePresenter.StartAuthorization(expiration);
		}
	}
}
