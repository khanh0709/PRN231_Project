using WebAPI.Business.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helper;

namespace WebClient.Pages.Admin
{
    public class DeleteTournamentModel : PageModel
    {
        private readonly APIHelper ApiHelper;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public DeleteTournamentModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                await ApiHelper.DeleteTournament(id);
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
