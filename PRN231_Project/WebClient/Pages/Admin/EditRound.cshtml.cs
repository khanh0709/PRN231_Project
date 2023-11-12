using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace WebClient.Pages.Admin
{
    public class EditRoundModel : PageModel
    {
        public List<RoundDTO> Rounds { get; set; }
        public List<MatchDTO> Matches { get; set; }
        public List<UserDTO> Players { get; set; }
        public int RoundId { get; set; }
        public int TourId { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        private readonly APIHelper ApiHelper;
        public EditRoundModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }

        public async Task<IActionResult> OnGet(int tourId)
        {
            TourId = tourId;
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = await ApiHelper.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour != null)
            {
                Players = await ApiHelper.GetPlayersInTournament(tourId, null);
                Rounds = await ApiHelper.GetRoundByTournamentId(tourId);
                if(Rounds != null && Rounds.Count > 0)
                {
                    RoundId = Rounds[0].RoundId;
                    Matches = await ApiHelper.GetMatchesByRoundId(RoundId);
                }
                return Page();
            }
            else
                return Redirect("/Admin/Home");
        }

        public async Task<IActionResult> OnPostSave(int tourId, int roundId, List<int?> player1Id, List<int?> player2Id, List<int?> winerId)
        {
            try
            {
                for (int i = 0; i < player1Id.Count; i++)
                {
                    if (player1Id[i] == 0)
                        player1Id[i] = null;
                    if (player2Id[i] == 0)
                        player2Id[i] = null;
                    if (winerId[i] == 0)
                        winerId[i] = null;
                }
                SaveMatchDTO model = new SaveMatchDTO()
                {
                    roundId = roundId,
                    player1Id = player1Id,
                    player2Id = player2Id,
                    winerId = winerId
                };
                await ApiHelper.SaveMatches(model);
                TempData["FlashMessage"] = "Lưu thành công!";
                TempData["TypeMessage"] = "success";
                return await OnPostMatch(tourId, roundId);
            }
            catch (Exception)
            {
                TempData["FlashMessage"] = "Lưu thất bại!";
                TempData["TypeMessage"] = "error";
                return await OnGet(tourId);
                throw;
            }
        }

        public async Task<IActionResult> OnPostMatch(int tourId, int roundId)
        {
            TourId = tourId;
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = await ApiHelper.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour != null)
            {
                Players = await ApiHelper.GetPlayersInTournament(tourId, null);
                Rounds = await ApiHelper.GetRoundByTournamentId(tourId);
                if (Rounds != null && Rounds.Count > 0)
                {
                    RoundId = roundId;
                    Matches = await ApiHelper.GetMatchesByRoundId(RoundId);
                }
                return Page();
            }
            else
                return Redirect("/Admin/Home");
        }

        public async Task<IActionResult> OnPostDelete(int tourId, int roundId)
        {
            try
            {
                await ApiHelper.DeleteRound(roundId);
                TempData["FlashMessage"] = "Xóa thành công!";
                TempData["TypeMessage"] = "success";
                return await OnGet(tourId);
            }
            catch (Exception)
            {
                TempData["FlashMessage"] = "Xóa thất bại!";
                TempData["TypeMessage"] = "error";
                return Page();
                throw;
            }
        }

        public async Task<IActionResult> OnPostGetUsers(string? term, int tourId)
        {
            var users = await ApiHelper.GetPlayersInTournament(tourId, term);
            users.Add(new UserDTO()
            {
                UserId = 0
            });
            var results = users.Select(user => new
            {
                Id = user.UserId,
                Text = user.UserId == 0 ? "Hủy Chọn" : (user.FullName + " (" + user.UserId + ")"),
            });
            return new JsonResult(results);
        }
    }
}
