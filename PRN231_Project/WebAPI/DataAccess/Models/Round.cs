using System;
using System.Collections.Generic;

namespace WebAPI.DataAccess.Models
{
    public partial class Round
    {
        public Round()
        {
            Matches = new HashSet<Match>();
        }

        public int RoundId { get; set; }
        public string? RoundName { get; set; }
        public int? TournamentId { get; set; }
        public int? MatchNumber { get; set; }

        public virtual Tournament? Tournament { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
