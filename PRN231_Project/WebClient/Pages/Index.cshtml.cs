using CoFAB.Business.DTO;
using CoFAB.Business.IRepository;
using CoFAB.Helper;
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
            this.UserRepository = UserRepository;
        }
        public void OnGet()
        {
            User = SessionHelper.GetUser(HttpContext.Session);
        }
    }
}