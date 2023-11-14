using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebClient.Helper;

namespace WebClient.Pages.Player
{
    public class EditProfileModel : PageModel
    {
        public IUserRepository UserRepository;
        public UserDTO User;
        public List<Province> Provinces;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        private HttpClient _httpClient;
        public EditProfileModel(IUserRepository UserRepository, HttpClient httpClient)
        {
            this.UserRepository = UserRepository;
            this._httpClient = httpClient;
        }
        public async Task OnGetAsync()
        {
            User = SessionHelper.GetUser(HttpContext.Session);

            var response = await _httpClient.GetAsync("https://provinces.open-api.vn/api/?depth=2");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Provinces = JsonConvert.DeserializeObject<List<Province>>(content);
            }
        }
        public async Task<IActionResult> OnPost(int id, string account, string fullName, string email, int province, string oldpassword, string password, string cfpassword)
        {
            try
            {
                UpdateProfileDTO profileDTO = new UpdateProfileDTO();
                if (!string.IsNullOrEmpty(oldpassword) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(cfpassword))
                {
                    if (password != cfpassword) throw new Exception("Mật khẩu mới không khớp!");
                    profileDTO.oldPass = oldpassword;
                    profileDTO.newPass = password;
                }
                else if (string.IsNullOrEmpty(oldpassword) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(cfpassword))
                {
                }
                else
                    throw new Exception("Nếu muốn thay đổi mật khẩu cần nhập đủ 3 trường cuối");

                profileDTO.id = id;
                profileDTO.name = fullName;
                profileDTO.email = email;
                profileDTO.city = province.ToString();

                string token = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "token");

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync("http://localhost:5184/api/User/EditProfile", profileDTO);


                if (!response.IsSuccessStatusCode)
                    throw new Exception("Sai mật khẩu!");

                response = await _httpClient.GetAsync($"http://localhost:5184/api/User/GetUsersById/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    User = JsonConvert.DeserializeObject<UserDTO>(content);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", User);
                }

                TempData["FlashMessage"] = "Lưu thành công!";
                TempData["TypeMessage"] = "success";
            }
            catch (Exception ex)
            {
                TempData["FlashMessage"] = "Lưu thất bại! " + ex.Message;
                TempData["TypeMessage"] = "error";
                return Redirect("/Player/EditProfile");
            }
            return Redirect("/Player/Home");
        }
    }
}
