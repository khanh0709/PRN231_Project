using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;
using WebClient.Helper;
using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Admin
{
    public class HomeModel : PageModel
    {
        private readonly APIHelper ApiHelper;

        public List<TournamentDTO> Tournaments;
        public HomeModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                UserDTO user = SessionHelper.GetUser(HttpContext.Session);
                Tournaments = await ApiHelper.GetTournamentsByUser(user.UserId);
                return Page();
            }
            catch (Exception e)
            {
                return RedirectToPage("/Error", new { message = "You can not access to this page!", backUrl = "/Player/Home" });
            }
        }
    }
}
