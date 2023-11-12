using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Common
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            SessionHelper.DeleteSession(HttpContext.Session, "user");
            SessionHelper.DeleteSession(HttpContext.Session, "token");
            return Redirect("/index");
        }
    }
}
