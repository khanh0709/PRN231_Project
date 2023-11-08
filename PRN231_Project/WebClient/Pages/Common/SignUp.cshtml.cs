using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;
using WebAPI.DataAccess.Models;
using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebClient.Pages.Common
{
    public class SignUpModel : PageModel
    {
        public IUserRepository UserRepository { get; set; }
        public List<Province> Provinces;
        public SignUpModel(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://provinces.open-api.vn/api/?depth=2");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Provinces = JsonConvert.DeserializeObject<List<Province>>(content);
                }
            }
        }
        public IActionResult OnPostSubmit(string fullname, string? email, string city, string acc, string password)
        {
            UserDTO u = UserRepository.GetUserByAcc(acc);
            if (u == null)
            {
                User newUser = new User();
                newUser.FullName = fullname;
                newUser.Email = email;
                newUser.City = city;
                newUser.Authorization = "player";
                newUser.Account = acc;
                newUser.Password = password;
                newUser.Role = (int?)UserRole.Player;
                UserRepository.AddPlayer(newUser);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", newUser);
                return new JsonResult(new { success = true, url = "/Player/Home" });
            }
            else
            {
                return new JsonResult(new { success = false });
            }

        }
    }
}
