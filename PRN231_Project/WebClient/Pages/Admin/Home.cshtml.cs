using CoFAB.Business.DTO;
using CoFAB.Business.Enums;
using CoFAB.Business.IRepository;
using CoFAB.Helper;
using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoFAB.Pages.Admin
{
    public class HomeModel : PageModel
    {
        public ITournamentRepository TournamentRepository;
        public List<TournamentDTO> Tournaments;
        public HomeModel(ITournamentRepository TournamentRepository)
        {
            this.TournamentRepository = TournamentRepository;
        }
        public void OnGet()
        {
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            Tournaments = TournamentRepository.GetTournamentsByUser(user.UserId);
        }
    }
}
