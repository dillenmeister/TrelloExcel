using System;
using TrelloNet;

namespace TrelloExcelAddIn
{
	public class CardInfo
	{
		public string Name { get; set; }
		public IListId ListId { get; set; }
		public string Desc { get; set; }
		public DateTime? Due { get; set; }
	}
}