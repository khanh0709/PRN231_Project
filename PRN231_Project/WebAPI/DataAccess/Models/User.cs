using System;
using System.Collections.Generic;

namespace WebAPI.DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Attemps = new HashSet<Attemp>();
            MatchPlayer1Navigations = new HashSet<Match>();
            MatchPlayer2Navigations = new HashSet<Match>();
            MatchWinerNavigations = new HashSet<Match>();
            Tournaments = new HashSet<Tournament>();
        }

        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Authorization { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }

        public virtual ICollection<Attemp> Attemps { get; set; }
        public virtual ICollection<Match> MatchPlayer1Navigations { get; set; }
        public virtual ICollection<Match> MatchPlayer2Navigations { get; set; }
        public virtual ICollection<Match> MatchWinerNavigations { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
