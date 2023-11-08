using WebAPI.Business.DTO;
using WebAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DataAccess.Manager
{
    public class MatchManager
    {
        CoFABContext context;
        public MatchManager(CoFABContext context)
        {
            this.context = context;
        }

        public List<Match> GetMatchesByRoundId(int roundId)
        {
            return context.Matches.Include(m => m.Player1Navigation).Include(m => m.Player2Navigation).Where(m => m.RoundId == roundId).ToList();
        }

        public void SaveMatches(int roundId, List<int?> player1Id, List<int?> player2Id, List<int?> winerId)
        {
            var matches = context.Matches.Where(m => m.RoundId == roundId).ToList();
            for(int i = 0; i < matches.Count; i++) 
            {
                matches[i].Player1 = player1Id[i];
                matches[i].Player2 = player2Id[i];
                matches[i].Winer = winerId[i];
            }
            context.Matches.UpdateRange(matches);
            context.SaveChanges();
        }
    }
}
