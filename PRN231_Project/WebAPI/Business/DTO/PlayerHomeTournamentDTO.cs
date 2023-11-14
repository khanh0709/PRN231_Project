namespace WebAPI.Business.DTO
{
    public class PlayerHomeTournamentDTO
    {
        public PlayerHomeTournamentDTO()
        {
            Attemps = new HashSet<PlayerHomeAttempDTO>();
            Rounds = new HashSet<RoundDTO>();
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
        public int? TotalWins { get; set; }
        public double? Xpgained { get; set; }
        public virtual FormatDTO? Format { get; set; }
        public virtual TypeDTO? Type { get; set; }
        public virtual UserDTO? User { get; set; }
        public virtual ICollection<PlayerHomeAttempDTO> Attemps { get; set; }
        public virtual ICollection<RoundDTO> Rounds { get; set; }
    }
}
