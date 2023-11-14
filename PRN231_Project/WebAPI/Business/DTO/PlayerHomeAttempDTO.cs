namespace WebAPI.Business.DTO
{
    public class PlayerHomeAttempDTO
    {
        public int AttempId { get; set; }
        public double? Xpgained { get; set; }
        public int? TournamentId { get; set; }
        public int? UserId { get; set; }
        public int? TotalWins { get; set; }
        public DateTime? Date { get; set; }
        public bool? Accepted { get; set; }
        
    }
}
