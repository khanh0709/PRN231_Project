using System;
using System.Collections.Generic;

namespace WebAPI.Business.DTO
{
    public partial class MatchDTO
    {
        public int MatchId { get; set; }
        public int? RoundId { get; set; }
        public int? Player1 { get; set; }
        public int? Player2 { get; set; }
        public int? Winer { get; set; }

        public virtual UserDTO? Player1Navigation { get; set; }
        public virtual UserDTO? Player2Navigation { get; set; }
        public virtual RoundDTO? Round { get; set; }
        public virtual UserDTO? WinerNavigation { get; set; }
    }
}
