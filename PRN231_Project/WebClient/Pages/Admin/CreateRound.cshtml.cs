using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;

namespace WebClient.Pages.Admin
{
    public class CreateRoundModel : PageModel
    {
        public IRoundRepository RoundRepository { get; set; }
        public ITournamentRepository TournamentRepository { get; set; }
        public int TourId { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public CreateRoundModel(IRoundRepository RoundRepository, ITournamentRepository TournamentRepository)
        {
            this.RoundRepository = RoundRepository;
            this.TournamentRepository = TournamentRepository;
        }
        public IActionResult OnGet(int tourId)
        {
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = TournamentRepository.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour == null)
            {
                return Redirect("/Admin/Home");
            }
            TourId = tourId;
            return Page();
        }

        public IActionResult OnPost(int tournamentId, string roundName, int matchNumber)
        {
            try
            {
                RoundRepository.CreateRound(tournamentId, roundName, matchNumber);
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
