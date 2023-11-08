using WebClient.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebClient.Helper;

namespace WebClient.Pages.Common
{
    public class LoginModel : PageModel
    {
        private readonly APIHelper ApiHelper;
        private readonly ISession session;
        public LoginModel(APIHelper ApiHelper, IHttpContextAccessor httpContextAccessor)
        {
            this.ApiHelper = ApiHelper;
            this.session = httpContextAccessor.HttpContext.Session;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostSubmit(string acc, string passWord)
        {
            try
            {
                LoginDTO model = new LoginDTO();
                model.Account = acc;
                model.Password = passWord;
                UserDTO user = await ApiHelper.Login(model);
                if (user != null)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", user);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "token", user.Token);
                    if (user.Role == (int)UserRole.Admin)
                    {
                        return new JsonResult(new { success = true, url = "/Admin/Home" });
                    }
                    else if (user.Role == (int)UserRole.Player)
                    {
                        return new JsonResult(new { success = true, url = "/Player/Home" });
                    }
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { success = false });
                throw;
            }
            return new JsonResult(new { success = false });
        }
    }
}
