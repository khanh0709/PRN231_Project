using WebAPI.Business.DTO;

namespace WebAPI.Business.IRepository
{
    public interface ITournamentRepository
    {
        public List<TournamentDTO> GetTournamentsByUser(int userId);
        public void CreateTournament(TournamentDTO tour);
        public TournamentDTO GetTournamentsByIdAndUser(int id, int userId);
        public void UpdateInfoTournament(TournamentDTO tour);
        public void DeleteTournament(int id);
        public bool ValidAcceptAndRemoveTournament(int tourId);
        public List<TournamentDTO> GetUpComingTournament(int userId);
        public List<TournamentDTO> GetRegisteredTournament(int userId);
        public List<TournamentDTO> GetEndTournament(int userId);
        public void UpdateStatus(int tourId, int status);
        public TournamentDTO GetTournamentById(int tourId);
    }
}
