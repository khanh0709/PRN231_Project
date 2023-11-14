using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;
using WebAPI.Business.Mapping;
using WebAPI.DataAccess.Manager;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.Repository
{
    public class UserRepository : IUserRepository
    {
        CoFABContext context;
        Mapper mapper;
        public UserRepository(CoFABContext context)
        {
            this.context = context;
            mapper = MapperConfig.InitializeAutomapper();
        }

        public UserDTO GetUserByAccAndPass(string id, string pass)
        {
            UserManager m = new UserManager(context);
            return mapper.Map<UserDTO>(m.GetUserByAccAndPass(id, pass));
        }

        public List<UserDTO> GetUsers(int role)
        {
            UserManager m = new UserManager(context);
            return mapper.Map<List<UserDTO>>(m.GetUsers(role));
        }
        public List<UserDTO> GetPlayers(string? term)
        {
            UserManager m = new UserManager(context);
            return mapper.Map<List<UserDTO>>(m.GetPlayers(term));
        }
        public List<UserDTO> GetPlayersInTournament(int tourId, string? term)
        {
            UserManager m = new UserManager(context);
            return mapper.Map<List<UserDTO>>(m.GetPlayersInTournament(tourId, term));
        }
        public UserDTO GetStatistic(UserDTO userDTO)
        {
            UserManager m = new UserManager(context);
            var users = mapper.Map<List<UserDTO>>(m.GetAllPlayers());
            DateTime ninetyDaysAgo = DateTime.Now.AddDays(-90);
            for (int i = 0; i < users.Count(); i++)
            {
                var attemps = users[i].Attemps.Where(a => a.Xpgained != null && a.TotalWins != null && a.Date != null);
                var attemps90 = users[i].Attemps.Where(a => a.Xpgained != null && a.TotalWins != null && a.Date >= ninetyDaysAgo);
                users[i].Score = attemps.Sum(a => a.Xpgained);
                users[i].Score90Day = attemps90.Sum(a => a.Xpgained);
                users[i].TotalWins = attemps.Sum(a => a.TotalWins);
            }
            UserDTO user = users.FirstOrDefault(u => u.UserId == userDTO.UserId);
            var usersCity = users.Where(u => u.City == user.City).ToList();

            user.GlobalRank = users.Count(a => a.Score > user.Score) + 1;
            user.GlobalRank90Day = users.Count(a => a.Score90Day > user.Score90Day) + 1;
            user.CityRank = usersCity.Count(a => a.Score > user.Score) + 1;
            user.CityRank90Day = usersCity.Count(a => a.Score90Day > user.Score90Day) + 1;
            return user;
        }
        public UserDTO GetUserByAcc(string acc)
        {
            UserManager m = new UserManager(context);
            return mapper.Map<UserDTO>(m.GetUserByAcc(acc));
        }

        public void AddPlayer(UserDTO u)
        {
            UserManager m = new UserManager(context);
            m.AddPlayer(mapper.Map<User>(u));
        }

        public void UpdateUser(UserDTO user)
        {
            UserManager manager = new UserManager(context);
            manager.UpdateUser(mapper.Map<User>(user));
        }

        public UserDTO GetUserById(int id)
        {
            UserManager manager = new UserManager(context);
            return mapper.Map<UserDTO>(manager.GetUserById(id));
        }
    }
}
