using WebAPI.Business.DTO;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.IRepository
{
    public interface IUserRepository
    {
        public UserDTO GetUserByAccAndPass(string id, string pass);
        public List<UserDTO> GetUsers(int role);
        public List<UserDTO> GetPlayers(string? term); //search by account and id
        public List<UserDTO> GetPlayersInTournament(int tourId, string? term);
        public UserDTO GetStatistic(UserDTO userDTO);
        public UserDTO GetUserByAcc(string acc);
        public void AddPlayer(User u);
        public void UpdateUser(UserDTO user);
    }
}
