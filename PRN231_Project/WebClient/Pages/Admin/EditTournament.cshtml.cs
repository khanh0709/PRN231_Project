using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;
using WebAPI.DataAccess.Models;
using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace WebClient.Pages.Admin
{
    public class EditTournamentModel : PageModel
    {
        public TournamentDTO Tournament;
        public List<AttempDTO> PendingPlayers;  
        public List<AttempDTO> RegisteredPlayers;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        private readonly APIHelper ApiHelper;

        public EditTournamentModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            UserDTO user = SessionHelper.GetUser(HttpContext.Session);
            Tournament = await ApiHelper.GetTournamentsByIdAndUser(id, user.UserId);
            await ApiHelper.UpdateStatus(Tournament.TournamentId, (int)TournamentStatus.InProgress);
            PendingPlayers = await ApiHelper.GetPlayers(id, false);
            RegisteredPlayers = await ApiHelper.GetPlayers(id, true);
            return Page();
        }

        public async Task<IActionResult> OnPostAcceptPlayer(string attempId)
        {
            try
            {
                if (!await ApiHelper.IsValidAcceptAndRemoveAttemp(int.Parse(attempId))) throw new Exception();
                await ApiHelper.UpdateAcceptAttemp(int.Parse(attempId), true);
                AttempDTO attemp = await ApiHelper.GetAttempById(int.Parse(attempId));
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

        public async Task<IActionResult> OnPostRemovePlayer(string attempId)
        {
            try
            {
                if (!await ApiHelper.IsValidAcceptAndRemoveAttemp(int.Parse(attempId))) throw new Exception();
                await ApiHelper.UpdateAcceptAttemp(int.Parse(attempId), false);
                AttempDTO attemp = await ApiHelper.GetAttempById(int.Parse(attempId));
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

        public async Task<IActionResult> OnPost(int tourId)
        {
            try
            {
                if(!await ApiHelper.ValidCalXp(tourId)) throw new Exception("Giải đấu đã được submit");
                await ApiHelper.CalXp(tourId);
                await ApiHelper.UpdateStatus(tourId, (int)TournamentStatus.End);
                TempData["FlashMessage"] = "Lưu thành công!";
                TempData["TypeMessage"] = "success";
                return Redirect("/Admin/Home");
            }
            catch (Exception ex)
            {
                TempData["FlashMessage"] = "Lưu thất bại! " + ex.Message;
                TempData["TypeMessage"] = "error";
                return await OnGet(tourId);
            }
        }
    }
}
