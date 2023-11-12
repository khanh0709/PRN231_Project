using System.Text.Json.Serialization;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.DTO
{
    public class FormatDTO
    {
        public FormatDTO()
        {
            Tournaments = new HashSet<TournamentDTO>();
        }

        public int FormatId { get; set; }
        public string? FormatName { get; set; }
        [JsonIgnore]
        public virtual ICollection<TournamentDTO> Tournaments { get; set; }
    }
}
