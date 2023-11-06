using System;
using System.Collections.Generic;

namespace CoFAB.DataAccess.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Attemps = new HashSet<Attemp>();
            Rounds = new HashSet<Round>();
        }

        public int TournamentId { get; set; }
        public string? Name { get; set; }
        public DateTime? StartTime { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public double? Xpmodifier { get; set; }
        public int? UserId { get; set; }
        public bool? Deleted { get; set; }
        public string? Address { get; set; }
        public int? TypeId { get; set; }
        public int? FormatId { get; set; }

        public virtual Format? Format { get; set; }
        public virtual Type? Type { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Attemp> Attemps { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
