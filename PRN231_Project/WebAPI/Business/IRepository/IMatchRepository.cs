using WebAPI.Business.DTO;

namespace WebAPI.Business.IRepository
{
    public interface IMatchRepository
    {
        public List<MatchDTO> GetMatchesByRoundId(int roundId);
        public void SaveMatches(int roundId, List<int?> player1Id, List<int?> player2Id, List<int?> winerId);
    }
}
