using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoFAB.Pages
{
    public class IndexModel : PageModel
    {
        public IUserRepository UserRepository;
        public UserDTO User { get; set; }
        public IndexModel(IUserRepository UserRepository)
        {
            //this.UserRepository = UserRepository;
        }
        public void OnGet()
        {
            //User = SessionHelper.GetUser(HttpContext.Session);
        }
    }
}