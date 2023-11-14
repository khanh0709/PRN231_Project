namespace WebAPI.Business.DTO
{
    public class UpdateProfileDTO
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? city { get; set; }
        public string? oldPass { get; set; }
        public string? newPass { get; set; }
    }
}
