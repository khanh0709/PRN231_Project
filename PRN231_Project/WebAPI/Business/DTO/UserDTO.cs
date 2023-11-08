using System;
using System.Collections.Generic;

namespace WebAPI.Business.DTO
{
    public partial class UserDTO
    {
        public UserDTO()
        {
            Attemps = new HashSet<AttempDTO>();
            MatchPlayer1Navigations = new HashSet<MatchDTO>();
            MatchPlayer2Navigations = new HashSet<MatchDTO>();
            MatchWinerNavigations = new HashSet<MatchDTO>();
            Tournaments = new HashSet<TournamentDTO>();
        }

        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Authorization { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string? CityName { get; set; }
        public int? CityRank { get; set; }
        public int? CityRank90Day { get; set; }
        public int? GlobalRank { get; set; }
        public int? GlobalRank90Day { get; set; }
        public double? Score { get; set; }
        public double? Score90Day { get; set; }
        public int? TotalWins { get; set; } 
        public string? Token { get; set; } 
        public virtual ICollection<AttempDTO> Attemps { get; set; }
        public virtual ICollection<MatchDTO> MatchPlayer1Navigations { get; set; }
        public virtual ICollection<MatchDTO> MatchPlayer2Navigations { get; set; }
        public virtual ICollection<MatchDTO> MatchWinerNavigations { get; set; }
        public virtual ICollection<TournamentDTO> Tournaments { get; set; }
    }
}
