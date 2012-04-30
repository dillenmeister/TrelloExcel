using System;

namespace TrelloExcelAddIn
{
	public class AuthorizationTokenReceivedArgs : EventArgs
	{
		private readonly string token;

		public AuthorizationTokenReceivedArgs(string token)
		{
			this.token = token;
		}

		public string Token
		{
			get { return token; }
		}
	}
}