using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebClient.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebClient.Pages.Player
{
    public class EditProfileModel : PageModel
    {
        public IUserRepository UserRepository;
        public UserDTO User;
        public List<Province> Provinces;
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        public EditProfileModel(IUserRepository UserRepository)
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
            }
        }
        public IActionResult OnPost(string account, string fullName, string email, int province, string oldpassword, string password, string cfpassword)
        {
            try
            {
                User = SessionHelper.GetUser(HttpContext.Session);
                if(!string.IsNullOrEmpty(oldpassword) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(cfpassword)) 
                {
                    UserDTO u = UserRepository.GetUserByAccAndPass(account, oldpassword);
                    if (password != cfpassword) throw new Exception("Mật khẩu mới không khớp!");
                    if (u == null) throw new Exception("Sai mật khẩu!");
                    User.Password = password;
                }
                else if(string.IsNullOrEmpty(oldpassword) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(cfpassword))
                {
                    
                }
                else
                    throw new Exception("Lỗi");
                User.FullName = fullName;
                User.Email = email;
                User.City = province.ToString();

                UserRepository.UpdateUser(User);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", User);
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
