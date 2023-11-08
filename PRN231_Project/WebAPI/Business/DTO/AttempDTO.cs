using System;
using System.Collections.Generic;

namespace WebAPI.Business.DTO
{
    public partial class AttempDTO
    {
        public int AttempId { get; set; }
        public double? Xpgained { get; set; }
        public int? TournamentId { get; set; }
        public int? UserId { get; set; }
        public int? TotalWins { get; set; }
        public DateTime? Date { get; set; }
        public bool? Accepted { get; set; }

        public virtual TournamentDTO? Tournament { get; set; }
        public virtual UserDTO? User { get; set; }
    }
}
