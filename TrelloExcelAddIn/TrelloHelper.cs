using System.Collections.Generic;
using System.Linq;
using TrelloNet;

namespace TrelloExcelAddIn
{
    public class TrelloHelper
    {
        private readonly ITrello trello;

        public TrelloHelper(ITrello trello)
        {
            this.trello = trello;
        }

        public IEnumerable<BoardViewModel> FetchBoardViewModelsForMe()
        {
            var boards = trello.Boards.ForMe(BoardFilter.Open).ToList();

            var organizations = boards
                .Select(b => b.IdOrganization)
                .Where(s => !string.IsNullOrEmpty(s))
                .Distinct()
                .Select(orgId =>
                {
                    try
                    {
                        return trello.Organizations.WithId(orgId);
                    }
                    catch (TrelloUnauthorizedException)
                    {
                        return null;
                    }
                })
                .Where(o => o != null)
                .ToDictionary(organization => organization.Id);

            return boards.Select(b =>
            {
                var model = new BoardViewModel(b);
                if (b.IdOrganization != null && organizations.ContainsKey(b.IdOrganization))
                    model.SetOrganizationName(organizations[b.IdOrganization].DisplayName);
                return model;
            });

        }
    }
}