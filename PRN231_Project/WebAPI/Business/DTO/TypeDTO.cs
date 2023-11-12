using System.Text.Json.Serialization;
using WebAPI.DataAccess.Models;

namespace WebAPI.Business.DTO
{
    public class TypeDTO
    {
        public TypeDTO()
        {
            Tournaments = new HashSet<TournamentDTO>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        [JsonIgnore]
        public virtual ICollection<TournamentDTO> Tournaments { get; set; }
    }
}
