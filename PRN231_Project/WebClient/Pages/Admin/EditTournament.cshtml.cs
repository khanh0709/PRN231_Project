using CoFAB.Business.DTO;
using CoFAB.Business.Enums;
using CoFAB.Business.IRepository;
using CoFAB.DataAccess.Models;
using CoFAB.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace CoFAB.Pages.Admin
{
    public class EditTournamentModel : PageModel
    {
        public ITournamentRepository TournamentRepository;
        public IAttempRepository AttempRepository;
        public TournamentDTO Tournament;
        public List<AttempDTO> PendingPlayers;  
        public List<AttempDTO> RegisteredPlayers;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public EditTournamentModel(ITournamentRepository TournamentRepository, IAttempRepository AttempRepository)
        {
            this.TournamentRepository = TournamentRepository;
            this.AttempRepository = AttempRepository;
        }
        public IActionResult OnGet(int id)
        {
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            Tournament = TournamentRepository.GetTournamentsByIdAndUser(id, user.UserId);
            TournamentRepository.UpdateStatus(Tournament.TournamentId, (int)TournamentStatus.InProgress);
            PendingPlayers = AttempRepository.GetPlayers(id, false);
            RegisteredPlayers = AttempRepository.GetPlayers(id, true);
            return Page();
        }

        public IActionResult OnPostAcceptPlayer(string attempId)
        {
            try
            {
                if (!AttempRepository.IsValidAcceptAndRemoveAttemp(int.Parse(attempId))) throw new Exception();
                AttempRepository.UpdateAcceptAttemp(int.Parse(attempId), true);
                AttempDTO attemp = AttempRepository.GetAttempById(int.Parse(attempId));
                return new JsonResult(new
                {
                    Account = attemp.User.FullName,
                    UserId = attemp.User.UserId,
                    TypeMessage = "success",
                    Message = "Thêm thành công!"
                });
            }
            catch (Exception)
            {
                return new JsonResult(new
                {
                    TypeMessage = "error",
                    Message = "Thêm thất bại!"
                });
            }
        }

        public IActionResult OnPostRemovePlayer(string attempId)
        {
            try
            {
                if (!AttempRepository.IsValidAcceptAndRemoveAttemp(int.Parse(attempId))) throw new Exception();
                AttempRepository.UpdateAcceptAttemp(int.Parse(attempId), false);
                AttempDTO attemp = AttempRepository.GetAttempById(int.Parse(attempId));
                return new JsonResult(new
                {
                    Account = attemp.User.FullName,
                    UserId = attemp.User.UserId,
                    TypeMessage = "success",
                    Message = "Xóa thành công!"
                });
            }
            catch (Exception)
            {
                return new JsonResult(new
                {
                    TypeMessage = "error",
                    Message = "Xóa thất bại!"
                });
            }
        }

        public IActionResult OnPost(int tourId)
        {
            try
            {
                if(!AttempRepository.ValidCalXp(tourId)) throw new Exception("Giải đấu đã được submit");
                AttempRepository.CalXp(tourId);
                TournamentRepository.UpdateStatus(tourId, (int)TournamentStatus.End);
                TempData["FlashMessage"] = "Lưu thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect("/Admin/Home");
            }
            catch (Exception ex)
            {
                TempData["FlashMessage"] = "Lưu thất bại! " + ex.Message;
                TempData["TypeMessage"] = "error";
                return OnGet(tourId);
            }
        }
    }
}
