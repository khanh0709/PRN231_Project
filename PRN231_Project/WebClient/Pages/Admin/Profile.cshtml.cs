using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebClient.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        public IUserRepository UserRepository;
        public UserDTO User;
        public List<Province> Provinces;
        public ProfileModel(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async Task OnGetAsync()
        {
            User = SessionHelper.GetUser(HttpContext.Session);
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
                }
            }
        }
        public void OnPost()
        {
            User = SessionHelper.GetUser(HttpContext.Session);
        }
    }
}
