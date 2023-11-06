using CoFAB.Business.DTO;
using CoFAB.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CoFAB.DataAccess.Manager
{
    public class AttempManager
    {
        CoFABContext context;
        public AttempManager(CoFABContext context)
        {
            this.context = context;
        }
        
        public List<Attemp> GetPlayers(int tournamentId, bool accepted)
        {
            return context.Attemps.Include(a => a.User).Where(a => a.TournamentId == tournamentId && a.Accepted == accepted).ToList();
        }

        public void UpdateAcceptAttemp(int attempId, bool accepted)
        {
            Attemp attemp = context.Attemps.FirstOrDefault(a => a.AttempId == attempId);  
            if (attemp != null) 
            {
                attemp.Accepted = accepted;

                context.Attemps.Update(attemp);
                context.SaveChanges();
            }
        }

        public Attemp GetAttempById(int attempId)
        {
            return context.Attemps.Include(a => a.User).FirstOrDefault(a => a.AttempId == attempId);
        }

        public void AddPlayers(int tournamentId, List<int> playerId)
        {
            foreach(int id in playerId) 
            {
                var attemp = context.Attemps.FirstOrDefault(a => a.TournamentId == tournamentId && a.UserId == id);
                if(attemp != null)
                {
                    attemp.Accepted = true;
                    context.Attemps.Update(attemp);
                }
                else
                {
                    attemp = new Attemp();
                    attemp.UserId = id;
                    attemp.TournamentId = tournamentId;
                    attemp.Accepted = true;
                    context.Attemps.Add(attemp);
                }
            }
            context.SaveChanges();
        }

        public bool IsValidAcceptAndRemoveAttemp(int attempId)
        {
            int? tourId = context.Attemps.FirstOrDefault(a => a.AttempId == attempId).TournamentId;
            if(tourId != null) 
            {
                var rounds = context.Rounds.Where(r => r.TournamentId == tourId);
                //has round => tournament is started
                if(rounds != null && rounds.Count() > 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void CalXp(int tourId)
        {
            double xpModifier = (double)context.Tournaments.FirstOrDefault(t => t.TournamentId == tourId).Xpmodifier;
            List<Attemp> attemps = context.Attemps.Where(a => a.TournamentId == tourId).ToList();
            for(int i = 0; i < attemps.Count; i++)
            {
                List<Match> matches = context.Matches.Where(m => m.Round.Tournament.TournamentId == tourId).ToList();
                foreach(Match match in matches)
                {
                    if (attemps[i].UserId == match.Winer)
                    {
                        if (attemps[i].TotalWins == null) attemps[i].TotalWins = 0;
                        attemps[i].TotalWins++;

                        if (attemps[i].Xpgained == null) attemps[i].Xpgained = 0;
                        attemps[i].Xpgained += xpModifier;
                    }
                }
                attemps[i].Date = DateTime.Now;
            }
            context.Attemps.UpdateRange(attemps);
            context.SaveChanges();
        }

        public bool ValidCalXp(int tourId)
        {
            var attemps = context.Attemps.Where(a => a.TournamentId == tourId && a.Xpgained != null);
            if(attemps != null && attemps.Count() > 0)
            {
                return false;
            }
            return true;
        }

        public void CreateRequest(int tourId, int userId)
        {
            Attemp attemp = new Attemp();
            attemp.TournamentId = tourId;
            attemp.UserId = userId;
            attemp.Accepted = false;

            context.Attemps.Add(attemp);
            context.SaveChanges();
        }

        public bool ValidRequest(int tourId, int userId)
        {
            var attemp = context.Attemps.FirstOrDefault(a => a.UserId == userId && a.TournamentId == tourId);
            if(attemp != null)
            {
                return false;
            }
            return true;
        }
    }
}
