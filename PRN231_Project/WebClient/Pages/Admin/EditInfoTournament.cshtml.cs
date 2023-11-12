using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;
using WebAPI.DataAccess.Models;
using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Admin
{
    public class EditInfoTournamentModel : PageModel
    {
        public List<TypeDTO> Types;
        public List<FormatDTO> Formats;
        public TournamentDTO Tournament;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        private readonly APIHelper ApiHelper;
        public EditInfoTournamentModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            Types = await ApiHelper.GetTypes();
            Formats = await ApiHelper.GetFormats();
            Tournament = await ApiHelper.GetTournamentsByIdAndUser(id, user.UserId);
            return Page();
        }
        public async Task<IActionResult> OnPost(int id, string name, int typeId, int formatId, DateTime startTime, string? description, string address, string xpmodifier)
        {
            try
            {
                TournamentDTO tour = new TournamentDTO()
                {
                    TournamentId = id,
                    Name = name,
                    TypeId = typeId,
                    FormatId = formatId,
                    StartTime = startTime,
                    Description = description,
                    Address = address,
                    Xpmodifier = double.Parse(xpmodifier)
                };
                await ApiHelper.UpdateInfoTournament(tour);
                TempData["FlashMessage"] = "Sửa thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect("/Admin/Home");
            }
            catch (Exception ex)
            {
                TempData["FlashMessage"] = "Sửa thất bại!";
                TempData["TypeMessage"] = "error";
                return await OnGet(id);
            }
        }
    }
}
