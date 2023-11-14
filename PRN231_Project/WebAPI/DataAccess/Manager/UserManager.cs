using Microsoft.EntityFrameworkCore;
using WebAPI.Business.Enums;
using WebAPI.DataAccess.Models;

namespace WebAPI.DataAccess.Manager
{
    public class UserManager
    {
        CoFABContext context;
        public UserManager(CoFABContext context)
        {
            this.context = context;
        }
        public User GetUserByAccAndPass(string acc, string pass)
        {
            return context.Users.FirstOrDefault(u => u.Account == acc && u.Password == pass);
        }

        public List<User> GetUsers(int role)
        {
            return context.Users.Where(u => u.Role == role).ToList();
        }

        public List<User> GetPlayers(string term)
        {
            if (term != null)
            {
                term = term.Trim().ToLower();
                return context.Users.Where(u => u.Role == (int)UserRole.Player && u.Account.ToLower().Contains(term) || u.UserId.ToString().Contains(term)).ToList();
            }
            return context.Users.Where(u => u.Role == (int)UserRole.Player).ToList();
        }

        public List<User> GetPlayersInTournament(int tourId, string? term)
        {
            if (term != null)
            {
                term = term.Trim().ToLower();
                return context.Attemps
                .Where(a => a.TournamentId == tourId && a.Accepted == true)
                .Select(a => a.User)
                .Where(u => u.Role == (int)UserRole.Player && u.Account.ToLower().Contains(term) || u.UserId.ToString().Contains(term))
                .ToList();
            }
            return context.Attemps
                .Where(a => a.TournamentId == tourId && a.Accepted == true)
                .Select(a => a.User)
                .ToList();
        }

        public User GetUserByAcc(string acc)
        {
            return context.Users.FirstOrDefault(u => u.Account == acc);
        }

        public void AddPlayer(User u)
        {
            context.Users.Add(u);
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var updateUser = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (updateUser != null)
            {
                updateUser.FullName = user.FullName;
                updateUser.Email = user.Email;
                updateUser.City = user.City;
                updateUser.Password = user.Password;

                context.Users.Update(updateUser);
                context.SaveChanges();
            }
        }

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public List<User> GetAllPlayers()
        {
            return context.Users.Include(u => u.Attemps).Where(u => u.Role == (int)UserRole.Player).ToList();
        }
    }
}
