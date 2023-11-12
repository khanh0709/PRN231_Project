using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;

namespace WebClient.Pages.Admin
{
    public class AddPlayersModel : PageModel
    {
        public List<UserDTO> Users { get; set; }
        public int TourId { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        private readonly APIHelper ApiHelper;
        public AddPlayersModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }

        public async Task<IActionResult> OnGet(int tourId)
        {
            Users = await ApiHelper.GetUsers((int)UserRole.Player);
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = await ApiHelper.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour == null)
            {
                return Redirect("/Admin/Home");
            }
            TourId = tourId;
            return Page();
        }

        public async Task<IActionResult> OnPost(List<int> playerId, int tourId)
        {
            try
            {
                if (playerId == null || playerId.Count == 0) throw new Exception("Vui lòng chọn người chơi!");
                if (!await ApiHelper.ValidAcceptAndRemoveTournament(tourId)) throw new Exception();
                await ApiHelper.AddPlayers(tourId, playerId);
                TempData["FlashMessage"] = "Thêm thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect($"/Admin/EditTournament?id={tourId}");
            }
            catch (Exception e)
            {
                TempData["FlashMessage"] = "Thêm thất bại! " + e.Message;
                TempData["TypeMessage"] = "error";
                return await OnGet(tourId);
            }
        }

        public async Task<IActionResult> OnPostGetUsers(string? term)
        {
            var users = await ApiHelper.GetPlayers(term);
            var results = users.Select(user => new
            {
                Id = user.UserId,
                Text = user.FullName + " (" + user.UserId + ")"
            });
            return new JsonResult(results);
        }
    }
}
