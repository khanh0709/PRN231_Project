using CoFAB.Business.DTO;
using CoFAB.Business.Enums;
using CoFAB.Business.IRepository;
using CoFAB.DataAccess.Models;
using CoFAB.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoFAB.Pages.Admin
{
    public class EditInfoTournamentModel : PageModel
    {
        public ITypeRepository TypeRepository;
        public IFormatRepository FormatRepository;
        public ITournamentRepository TournamentRepository;
        public List<TypeDTO> Types;
        public List<FormatDTO> Formats;
        public TournamentDTO Tournament;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public EditInfoTournamentModel(ITypeRepository TypeRepository, IFormatRepository FormatRepository, ITournamentRepository TournamentRepository)
        {
            this.TypeRepository = TypeRepository;
            this.FormatRepository = FormatRepository;
            this.TournamentRepository = TournamentRepository;
        }
        public IActionResult OnGet(int id)
        {
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            Types = TypeRepository.GetTypes();
            Formats = FormatRepository.GetFormats();
            Tournament = TournamentRepository.GetTournamentsByIdAndUser(id, user.UserId);
            return Page();
        }
        public IActionResult OnPost(int id, string name, int typeId, int formatId, DateTime startTime, string? description, string address, string xpmodifier)
        {
            try
            {
                TournamentRepository.UpdateInfoTournament(id, name, typeId, formatId, startTime, description, address, double.Parse(xpmodifier));
                TempData["FlashMessage"] = "Sửa thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect("/Admin/Home");
            }
            catch (Exception ex)
            {
                TempData["FlashMessage"] = "Sửa thất bại!";
                TempData["TypeMessage"] = "error";
                return OnGet(id);
            }
        }
    }
}
