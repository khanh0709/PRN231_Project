using WebAPI.Business.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Admin
{
    public class DeleteTournamentModel : PageModel
    {
        public ITournamentRepository TournamentRepository { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public DeleteTournamentModel(ITournamentRepository TournamentRepository)
        {
            this.TournamentRepository = TournamentRepository;
        }
        public IActionResult OnGet(int id)
        {
            try
            {
                TournamentRepository.DeleteTournament(id);
                TempData["FlashMessage"] = "Xóa thành công!";
                TempData["TypeMessage"] = "success";
            }
            catch (Exception)
            {
                TempData["FlashMessage"] = "Xóa thất bại!";
                TempData["TypeMessage"] = "error";
            }
            return Redirect("/Admin/Home");
        }
    }
}
