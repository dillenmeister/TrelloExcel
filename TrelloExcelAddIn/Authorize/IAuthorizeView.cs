using System;

namespace TrelloExcelAddIn
{
	public interface IAuthorizeView
	{
		event AuthorizationTokenReceivedEventHandler AuthorizationTokenReceived;		
		void ShowAuthorizationDialog(Uri url);
		void Hide();
	}
}