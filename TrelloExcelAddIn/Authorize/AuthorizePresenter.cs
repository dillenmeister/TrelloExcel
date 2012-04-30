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
				authorizeView.Hide();
				messageBus.Publish(new TrelloWasAuthorizedEvent());
			};
		}

		public void StartAuthorization()
		{
			var url = trello.GetAuthorizationUrl("TrelloExcel", Scope.ReadWrite, Expiration.OneHour);
			authorizeView.ShowAuthorizationDialog(url);
		}
	}
}