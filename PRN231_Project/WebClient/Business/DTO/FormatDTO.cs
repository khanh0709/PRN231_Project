using CoFAB.DataAccess.Models;

namespace CoFAB.Business.DTO
{
    public class FormatDTO
    {
        public FormatDTO()
        {
            Tournaments = new HashSet<TournamentDTO>();
        }

        public int FormatId { get; set; }
        public string? FormatName { get; set; }

        public virtual ICollection<TournamentDTO> Tournaments { get; set; }
    }
}
