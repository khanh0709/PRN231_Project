using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAPI.Business.DTO;
using WebClient.Helper;

namespace WebClient.Pages.Common
{
    public class RankingModel : PageModel
    {
        public List<Province> Provinces;
        public List<UserDTO> Players;
        public string City;
        public string Term;
        private readonly APIHelper ApiHelper;
        public RankingModel(APIHelper ApiHelper)
        {
            this.ApiHelper = ApiHelper;
        }

        public async Task<IActionResult> OnGet()
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
            return Page();
        }
    }
}
