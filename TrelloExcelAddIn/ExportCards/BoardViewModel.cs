using TrelloNet;

namespace TrelloExcelAddIn
{
	public class BoardViewModel : IBoardId
	{
		private readonly Board board;		
		private string organizationName;

		public BoardViewModel(Board board)
		{
			this.board = board;			
		}

		public void SetOrganizationName(string name)
		{
			organizationName = name;
		}

		public override string ToString()
		{
			if (string.IsNullOrWhiteSpace(organizationName))
				return board.Name;

			return string.Format("{0} ({1})", board.Name, organizationName);
		}

		public string GetBoardId()
		{
			return board.GetBoardId();
		}
	}
}