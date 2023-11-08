using System;
using System.Collections.Generic;

namespace WebAPI.Business.DTO
{
    public partial class RoundDTO
    {
        public RoundDTO()
        {
            Matches = new HashSet<MatchDTO>();
        }

        public int RoundId { get; set; }
        public string? RoundName { get; set; }
        public int? TournamentId { get; set; }
        public int? MatchNumber { get; set; }
        public virtual TournamentDTO? Tournament { get; set; }
        public virtual ICollection<MatchDTO> Matches { get; set; }
    }
}
