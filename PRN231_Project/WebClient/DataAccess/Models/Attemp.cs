using System;
using System.Collections.Generic;

namespace CoFAB.DataAccess.Models
{
    public partial class Attemp
    {
        public int AttempId { get; set; }
        public double? Xpgained { get; set; }
        public int? TournamentId { get; set; }
        public int? UserId { get; set; }
        public int? TotalWins { get; set; }
        public DateTime? Date { get; set; }
        public bool? Accepted { get; set; }

        public virtual Tournament? Tournament { get; set; }
        public virtual User? User { get; set; }
    }
}
