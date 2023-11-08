using WebAPI.Business.DTO;
using Newtonsoft.Json;

namespace WebClient.Helper
{
    public class APIHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ISession _session;
        public APIHelper(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _session = httpContextAccessor.HttpContext.Session;
            if (!string.IsNullOrEmpty(_session.GetString("token")))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _session.GetString("token"));
            }
        }
        public async Task<UserDTO> Login(LoginDTO model)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/Login", model);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            UserDTO result = JsonConvert.DeserializeObject<UserDTO>(responseBody);
            return result;
        }

        public async Task<List<TournamentDTO>> GetTournamentsByUser(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Tournament/GetTournamentsByUser/{userId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<TournamentDTO> result = JsonConvert.DeserializeObject<List<TournamentDTO>>(responseBody);
            return result;
        }
    }
}
