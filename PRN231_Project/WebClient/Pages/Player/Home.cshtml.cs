﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;
using WebClient.Helper;

namespace WebClient.Pages.Player
{
    public class HomeModel : PageModel
    {
        public ITournamentRepository TournamentRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IAttempRepository AttempRepository { get; set; }
        public List<TournamentDTO> UpComingTour { get; set; }
        public List<TournamentDTO> RegisteredTour { get; set; }
        public List<TournamentDTO> EndTour { get; set; }
        public UserDTO User { get; set; }
        public string FlashMessage { get; set; }
        public string TypeMessage { get; set; }
        private readonly APIHelper ApiHelper;

        public HomeModel(ITournamentRepository TournamentRepository, IUserRepository UserRepository, IAttempRepository AttempRepository, APIHelper ApiHelper)
        {
            this.TournamentRepository = TournamentRepository;
            this.UserRepository = UserRepository;
            this.AttempRepository = AttempRepository;
            this.ApiHelper = ApiHelper;
        }
        public async Task<IActionResult> OnGet()
        {
            User = SessionHelper.GetUser(HttpContext.Session);
            if (User != null && User.Role == (int)UserRole.Player)
            {
                try
                {
                    User = await ApiHelper.GetStatistic(User);
                    UpComingTour = await ApiHelper.GetUpComingTournament(User.UserId);
                    RegisteredTour = await ApiHelper.GetRegisteredTournament(User.UserId);
                    EndTour = await ApiHelper.GetEndTournament(User.UserId);
                }
                catch (Exception e)
                {
                    return RedirectToPage("/Error", new { message = "Some error occured!", backUrl = "/Common/Login" });
                }

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://provinces.open-api.vn/api/?depth=2");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        List<Province> provinces = JsonConvert.DeserializeObject<List<Province>>(content);
                        var province = provinces.FirstOrDefault(p => p.Code == User.City);
                        User.CityName = province.Name;
                    }
                }
                return Page();
            }
            TempData["FlashMessage"] = "Không Thể Truy Cập!";
            TempData["TypeMessage"] = "error";
            return Redirect("/Common/Login");
        }

        public async Task<IActionResult> OnPostRequest(int tourId)
        {
            try
            {
                UserDTO user = SessionHelper.GetUser(HttpContext.Session);

                var check = await ApiHelper.CheckValidRequest(tourId, user.UserId);
                if (!check)
                {
                    throw new Exception("Đã gửi request, vui lòng chờ admin!");
                }
                await ApiHelper.CreateRequest(tourId, user.UserId);
                TempData["FlashMessage"] = "Gửi request thành công!";
                TempData["TypeMessage"] = "success";
            }
            catch (Exception ex)
            {
                TempData["FlashMessage"] = ex.Message ?? "Gửi request lỗi!";
                TempData["TypeMessage"] = "error";
            }

            await OnGet();
            return Page();
        }
    }
}
