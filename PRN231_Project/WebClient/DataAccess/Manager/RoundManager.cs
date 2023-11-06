using CoFAB.Business.DTO;
using CoFAB.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoFAB.DataAccess.Manager
{
    public class RoundManager
    {
        CoFABContext context;
        public RoundManager(CoFABContext context)
        {
            this.context = context;
        }

        public void CreateRound(int tournamentId, string roundName, int matchNumber)
        {
            Round round = new Round();  
            round.TournamentId = tournamentId;
            round.RoundName = roundName;
            round.MatchNumber = matchNumber;
            context.Rounds.Add(round);
            context.SaveChanges();  
            
            List<Match> matches = new List<Match>();    
            for(int i = 0; i < matchNumber; i++) 
            {
                Match match = new Match();
                match.RoundId = round.RoundId;
                matches.Add(match);
            }
            context.Matches.AddRange(matches);
            context.SaveChanges();
        }

        public List<Round> GetRoundByTournamentId(int tourId)
        {
            return context.Rounds.Where(r => r.TournamentId == tourId).ToList();    
        }

        public void DeleteRound(int roundId)
        {
            var roundToDelete = context.Rounds
               .Include(r => r.Matches)
               .FirstOrDefault(r => r.RoundId == roundId);

            if (roundToDelete == null)
            {
                return;
            }

            context.Matches.RemoveRange(roundToDelete.Matches);
            context.Rounds.Remove(roundToDelete);
            context.SaveChanges();

        }
    }
}
