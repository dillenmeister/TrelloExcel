using System;

namespace TrelloExcelAddIn
{
	public class TrelloWasUnauthorizedEvent
	{
	    public TrelloWasUnauthorizedEvent(string message)
	    {
	        Message = message;
	    }

	    public string Message { get; private set; }
	}
}