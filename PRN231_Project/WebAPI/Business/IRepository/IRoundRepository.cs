using WebAPI.Business.DTO;

namespace WebAPI.Business.IRepository
{
    public interface IRoundRepository
    {
        public void CreateRound(RoundDTO round);
        public List<RoundDTO> GetRoundByTournamentId(int tourId);
        public void DeleteRound(int roundId);
    }
}
