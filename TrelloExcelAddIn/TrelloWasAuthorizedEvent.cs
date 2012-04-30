using TrelloNet;

namespace TrelloExcelAddIn
{
	public class TrelloWasAuthorizedEvent
	{
		private readonly Member member;

		public TrelloWasAuthorizedEvent(Member member)
		{
			this.member = member;
		}

		public Member Member
		{
			get { return member; }
		}
	}
}