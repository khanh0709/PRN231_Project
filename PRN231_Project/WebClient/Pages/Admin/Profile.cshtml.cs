using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebClient.Helper;

namespace WebClient.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        public UserDTO User;
        public List<Province> Provinces;
        public string err = "";
        public string token = "";
        public ProfileModel(IUserRepository UserRepository)
        {
        }
        public async Task<IActionResult> OnGetAsync()
        {
            User = SessionHelper.GetUser(HttpContext.Session);
            token = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "token");

            if (User == null)
            {
                return Redirect("/Common/Login");
            }
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://provinces.open-api.vn/api/?depth=2");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Provinces = JsonConvert.DeserializeObject<List<Province>>(content);
                }
                else
                {
                    err = "Failed to load";
                }
            }
            return Page();
        }
        public void OnPost()
        {
            User = SessionHelper.GetUser(HttpContext.Session);
        }
    }
}
