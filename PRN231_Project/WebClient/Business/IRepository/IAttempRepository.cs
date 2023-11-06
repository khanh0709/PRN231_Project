using CoFAB.Business.DTO;

namespace CoFAB.Business.IRepository
{
    public interface IAttempRepository
    {
        public List<AttempDTO> GetPlayers(int tournamentId, bool accepted);
        public void UpdateAcceptAttemp(int attempId, bool accepted);
        public AttempDTO GetAttempById(int attempId);
        public void AddPlayers(int tournamentId, List<int> playerId);
        public bool IsValidAcceptAndRemoveAttemp(int attempId);
        public void CalXp(int tourId);
        public bool ValidCalXp(int tourId);
        public void CreateRequest(int tourId, int userId);
        public bool ValidRequest(int tourId, int userId);   
    }
}
