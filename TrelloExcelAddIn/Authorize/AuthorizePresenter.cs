using TrelloExcelAddIn.Properties;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public class AuthorizePresenter
	{
		private readonly IAuthorizeView authorizeView;
		private readonly ITrello trello;
		private readonly IMessageBus messageBus;

		public AuthorizePresenter(IAuthorizeView authorizeView, ITrello trello, IMessageBus messageBus)
		{
			this.authorizeView = authorizeView;
			this.trello = trello;
			this.messageBus = messageBus;			

			authorizeView.AuthorizationTokenReceived += (sender, args) =>
			{
				trello.Authorize(args.Token);
				StoreTokenInSettings(args.Token);

				authorizeView.Hide();

				trello.Async.Members.Me()
					.ContinueWith(t => messageBus.Publish(new TrelloWasAuthorizedEvent(t.Result)));
			};
		}

		public void StartAuthorization(Expiration expiration)
		{
			var url = trello.GetAuthorizationUrl("TrelloExcel", Scope.ReadWrite, expiration);
			authorizeView.ShowAuthorizationDialog(url);
		}

		private static void StoreTokenInSettings(string token)
		{
			Settings.Default.Token = token;
			Settings.Default.Save();
		}
	}
}