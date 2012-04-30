using TrelloNet;

namespace TrelloExcelAddIn
{
	public class AuthorizePresenter
	{
		private readonly IAuthorizeView authorizeView;
		private readonly ITrello trello;

		public AuthorizePresenter(IAuthorizeView authorizeView, ITrello trello)
		{
			this.authorizeView = authorizeView;
			this.trello = trello;

			authorizeView.AuthorizationTokenReceived += (sender, args) =>
			{
				trello.Authorize(args.Token);
				authorizeView.Hide();
			};
		}

		public void StartAuthorization()
		{
			var url = trello.GetAuthorizationUrl("TrelloExcel", Scope.ReadWrite, Expiration.OneHour);

			authorizeView.ShowAuthorizationDialog(url);
		}
	}
}