namespace WebAPI.Business.DTO
{
    public class SaveMatchDTO
    {
        public int roundId { get; set; }
        public List<int?> player1Id { get; set; }
        public List<int?> player2Id { get; set; }
        public List<int?> winerId { get; set; }

    }
}
