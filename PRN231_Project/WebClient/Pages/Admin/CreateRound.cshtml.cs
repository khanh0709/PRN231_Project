using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;

namespace WebClient.Pages.Admin
{
    public class CreateRoundModel : PageModel
    {
        public int TourId { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        private readonly APIHelper ApiHelper;
        public CreateRoundModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }
        public async Task<IActionResult> OnGet(int tourId)
        {
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = await ApiHelper.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour == null)
            {
                return Redirect("/Admin/Home");
            }
            TourId = tourId;
            return Page();
        }

        public async Task<IActionResult> OnPost(int tournamentId, string roundName, int matchNumber)
        {
            try
            {
                RoundDTO round = new RoundDTO()
                {
                    TournamentId = tournamentId,
                    RoundName = roundName,  
                    MatchNumber = matchNumber   
                };
                await ApiHelper.CreateRound(round);
                TempData["FlashMessage"] = "Tạo thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect($"/Admin/EditTournament?Id={tournamentId}");
            }
            catch (Exception)
            {
                TempData["FlashMessage"] = "Tạo thất bại!";
                TempData["TypeMessage"] = "error";
                return Page();
            }
        }
    }
}
