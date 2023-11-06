using CoFAB.Business.DTO;
using CoFAB.Business.IRepository;
using CoFAB.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace CoFAB.Pages.Admin
{
    public class EditRoundModel : PageModel
    {
        public IRoundRepository RoundRepository { get; set; }   
        public IMatchRepository MatchRepository { get; set; }   
        public ITournamentRepository TournamentRepository { get; set; } 
        public IUserRepository UserRepository { get; set; }
        public List<RoundDTO> Rounds { get; set; }
        public List<MatchDTO> Matches { get; set; }
        public List<UserDTO> Players { get; set; }
        public int RoundId { get; set; }
        public int TourId { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public EditRoundModel(IMatchRepository MatchRepository, IRoundRepository RoundRepository, ITournamentRepository TournamentRepository, IUserRepository UserRepository)
        {
            this.RoundRepository = RoundRepository;
            this.MatchRepository = MatchRepository;
            this.TournamentRepository = TournamentRepository;
            this.UserRepository = UserRepository;
        }

        public IActionResult OnGet(int tourId)
        {
            TourId = tourId;
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = TournamentRepository.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour != null)
            {
                Players = UserRepository.GetPlayersInTournament(tourId, null);
                Rounds = RoundRepository.GetRoundByTournamentId(tourId);
                if(Rounds != null && Rounds.Count > 0)
                {
                    RoundId = Rounds[0].RoundId;
                    Matches = MatchRepository.GetMatchesByRoundId(RoundId);
                }
                return Page();
            }
            else
                return Redirect("/Admin/Home");
        }

        public IActionResult OnPostSave(int tourId, int roundId, List<int?> player1Id, List<int?> player2Id, List<int?> winerId)
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
                MatchRepository.SaveMatches(roundId, player1Id, player2Id, winerId);
                TempData["FlashMessage"] = "Lưu thành công!";
                TempData["TypeMessage"] = "success";
                return OnPostMatch(tourId, roundId);
            }
            catch (Exception)
            {
                TempData["FlashMessage"] = "Lưu thất bại!";
                TempData["TypeMessage"] = "error";
                return OnGet(tourId);
                throw;
            }
        }

        public IActionResult OnPostMatch(int tourId, int roundId)
        {
            TourId = tourId;
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            TournamentDTO tour = TournamentRepository.GetTournamentsByIdAndUser(tourId, user.UserId);
            if (tour != null)
            {
                Players = UserRepository.GetPlayersInTournament(tourId, null);
                Rounds = RoundRepository.GetRoundByTournamentId(tourId);
                if (Rounds != null && Rounds.Count > 0)
                {
                    RoundId = roundId;
                    Matches = MatchRepository.GetMatchesByRoundId(RoundId);
                }
                return Page();
            }
            else
                return Redirect("/Admin/Home");
        }

        public IActionResult OnPostDelete(int tourId, int roundId)
        {
            try
            {
                RoundRepository.DeleteRound(roundId);
                TempData["FlashMessage"] = "Xóa thành công!";
                TempData["TypeMessage"] = "success";
                return OnGet(tourId);
            }
            catch (Exception)
            {
                TempData["FlashMessage"] = "Xóa thất bại!";
                TempData["TypeMessage"] = "error";
                return Page();
                throw;
            }
        }

        public IActionResult OnPostGetUsers(string? term, int tourId)
        {
            var users = UserRepository.GetPlayersInTournament(tourId, term);
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
