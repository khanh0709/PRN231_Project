using System;
using System.Collections.Generic;

namespace CoFAB.DataAccess.Models
{
    public partial class Match
    {
        public int MatchId { get; set; }
        public int? RoundId { get; set; }
        public int? Player1 { get; set; }
        public int? Player2 { get; set; }
        public int? Winer { get; set; }

        public virtual User? Player1Navigation { get; set; }
        public virtual User? Player2Navigation { get; set; }
        public virtual Round? Round { get; set; }
        public virtual User? WinerNavigation { get; set; }
    }
}
