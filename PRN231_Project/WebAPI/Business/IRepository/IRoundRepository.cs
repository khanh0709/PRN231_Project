using WebAPI.Business.DTO;

namespace WebAPI.Business.IRepository
{
    public interface IRoundRepository
    {
        public void CreateRound(int tournamentId, string roundName, int matchNumber);
        public List<RoundDTO> GetRoundByTournamentId(int tourId);
        public void DeleteRound(int roundId);
    }
}
