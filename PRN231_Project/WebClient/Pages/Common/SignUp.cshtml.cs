using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;
using WebClient.Helper;

namespace WebClient.Pages.Common
{
    public class SignUpModel : PageModel
    {
        public IUserRepository UserRepository { get; set; }
        public List<Province> Provinces;
        private HttpClient _httpClient;

        public SignUpModel(IUserRepository userRepository, HttpClient httpClient)
        {
            UserRepository = userRepository;
            _httpClient = httpClient;
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
        public async Task<IActionResult> OnPostSubmit(string fullname, string? email, string city, string acc, string password)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5184/api/User/GetUsersByAcc/{acc}");
            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = false });
            }
            UserDTO newUser = new UserDTO();
            newUser.FullName = fullname;
            newUser.Email = email;
            newUser.City = city;
            newUser.Authorization = "player";
            newUser.Account = acc;
            newUser.Password = password;
            newUser.Role = (int?)UserRole.Player;

            response = await _httpClient.PostAsJsonAsync("http://localhost:5184/api/User/AddPlayer", newUser);
            if (!response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = false });
            }

            response = await _httpClient.GetAsync($"http://localhost:5184/api/User/GetUsersByAcc/{acc}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(content);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", user);
            }
            return new JsonResult(new { success = true, url = "/Player/Home" });
        }
    }
}
