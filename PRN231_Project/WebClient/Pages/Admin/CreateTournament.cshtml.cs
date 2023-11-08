using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebAPI.Business.Enums;

namespace WebClient.Pages.Admin
{
    public class CreateTournamentModel : PageModel
    {
        public ITypeRepository TypeRepository;
        public IFormatRepository FormatRepository;
        public ITournamentRepository TournamentRepository;
        public List<TypeDTO> Types;
        public List<FormatDTO> Formats;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public CreateTournamentModel(ITypeRepository TypeRepository, IFormatRepository FormatRepository, ITournamentRepository TournamentRepository) 
        {
            this.TypeRepository = TypeRepository;
            this.FormatRepository = FormatRepository;
            this.TournamentRepository = TournamentRepository;
        }
        public IActionResult OnGet()
        {
            Types = TypeRepository.GetTypes();
            Formats = FormatRepository.GetFormats();
            return Page();
        }
        public IActionResult OnPost(string name, int typeId, int formatId, DateTime startTime, string? description, string address, string xpmodifier)
        {
            try
            {
                UserDTO user = SessionHelper.GetUser(HttpContext.Session);

                TournamentDTO tour = new TournamentDTO();
                tour.Name = name;
                tour.TypeId = typeId;
                tour.FormatId = formatId;
                tour.StartTime = startTime;
                tour.Description = description;
                tour.UserId = user.UserId;
                tour.Status = (int)TournamentStatus.UpComing;
                tour.Address = address;
                tour.Xpmodifier = double.Parse(xpmodifier);
                tour.Deleted = false;

                TournamentRepository.CreateTournament(tour);
                TempData["FlashMessage"] = "Tạo thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect("/Admin/Home");
            }
            catch (Exception ex) 
            {
                TempData["FlashMessage"] = "Tạo thất bại!";
                TempData["TypeMessage"] = "error";
                return OnGet();
            }
        }
    }
}
