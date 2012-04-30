using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TrelloExcelAddIn
{
	public partial class AuthorizationDialog : Form, IAuthorizeView
	{
		public AuthorizationDialog()
		{
			InitializeComponent();

			WebBrowser.DocumentCompleted += WebBrowserOnDocumentCompleted;

			FormClosing += (sender, args) =>
			{
				if (args.CloseReason != CloseReason.UserClosing) return;
				args.Cancel = true;
				Hide();
			};
		}

		private void WebBrowserOnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs args)
		{
			if (args.Url == new Uri("https://trello.com/1/token/approve"))
			{
				var token = ParseTokenFromWebpage(WebBrowser.Document);
				if(token != null)
					AuthorizationTokenReceived.Invoke(this, new AuthorizationTokenReceivedArgs(token));
			}
		}

		private string ParseTokenFromWebpage(HtmlDocument document)
		{
			var match = Regex.Match(document.Body.InnerText, "[a-z0-9]{64}");

			return match.Success ? match.ToString() : null;
		}

		public event AuthorizationTokenReceivedEventHandler AuthorizationTokenReceived;

		public void ShowAuthorizationDialog(Uri url)
		{
			Show();
			WebBrowser.Navigate(url);
		}
	}

	public delegate void AuthorizationTokenReceivedEventHandler(object sender, AuthorizationTokenReceivedArgs args);
}
