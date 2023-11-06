using CoFAB.Business.DTO;
using CoFAB.Business.Enums;
using CoFAB.Business.IRepository;
using CoFAB.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoFAB.Pages.Common
{
    public class LoginModel : PageModel
    {
        public IUserRepository UserRepository;
        public LoginModel(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPostSubmit(string acc, string passWord)
        {
            UserDTO u = UserRepository.GetUserByAccAndPass(acc, passWord);
            if (u != null)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", u);
                if (u.Role == (int)UserRole.Admin)
                {
                    return new JsonResult(new { success = true, url = "/Admin/Home" });
                }
                else if (u.Role == (int)UserRole.Player)
                {
                    return new JsonResult(new { success = true, url = "/Player/Home" });
                }
            }
            return new JsonResult(new { success = false});
        }
    }
}
