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
        public IUserRepository UserRepository { get; set; }
        public ITournamentRepository TournamentRepository { get; set; }
        public IAttempRepository AttempRepository { get; set; }
        public List<UserDTO> Users { get; set; }
        public int TourId { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public AddPlayersModel(IUserRepository UserRepository, ITournamentRepository TournamentRepository, IAttempRepository AttempRepository)
        {
            this.UserRepository = UserRepository;
            this.TournamentRepository = TournamentRepository;
            this.AttempRepository = AttempRepository;
        }

        public IActionResult OnGet(int tourId)
        {
            Users = UserRepository.GetUsers((int)UserRole.Player);
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = TournamentRepository.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour == null)
            {
                return Redirect("/Admin/Home");
            }
            TourId = tourId;
            return Page();
        }

        public IActionResult OnPost(List<int> playerId, int tourId)
        {
            try
            {
                if (!TournamentRepository.ValidAcceptAndRemoveTournament(tourId)) throw new Exception();
                AttempRepository.AddPlayers(tourId, playerId);
                TempData["FlashMessage"] = "Thêm thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect($"/Admin/EditTournament?id={tourId}");
            }
            catch (Exception)
            {
                TempData["FlashMessage"] = "Thêm thất bại!";
                TempData["TypeMessage"] = "error";
                return OnGet(tourId);
            }
        }

        public IActionResult OnPostGetUsers(string? term)
        {
            var users = UserRepository.GetPlayers(term);
            var results = users.Select(user => new
            {
                Id = user.UserId,
                Text = user.FullName + " (" + user.UserId + ")"
            });
            return new JsonResult(results);
        }
    }
}
