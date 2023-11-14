using Newtonsoft.Json;
using WebAPI.Business.DTO;

namespace WebClient.Helper
{
    public class APIHelper
    {
        private HttpClient _httpClient;
        private ISession _session;
        public APIHelper(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _session = httpContextAccessor.HttpContext.Session;
            string token = SessionHelper.GetObjectFromJson<string>(_session, "token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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

        public async Task<List<TypeDTO>> GetTypes()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Type/GetTypes");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<TypeDTO> result = JsonConvert.DeserializeObject<List<TypeDTO>>(responseBody);
            return result;
        }

        public async Task<List<FormatDTO>> GetFormats()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Format/GetFormats");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<FormatDTO> result = JsonConvert.DeserializeObject<List<FormatDTO>>(responseBody);
            return result;
        }

        public async Task CreateTournament(TournamentDTO tour)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Tournament/CreateTournament", tour);
            response.EnsureSuccessStatusCode();
        }

        public async Task<TournamentDTO> GetTournamentsByIdAndUser(int tourId, int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Tournament/GetTournamentsByIdAndUser/{tourId}/{userId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            TournamentDTO result = JsonConvert.DeserializeObject<TournamentDTO>(responseBody);
            return result;
        }

        public async Task UpdateInfoTournament(TournamentDTO tour)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Tournament/UpdateInfoTournament", tour);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTournament(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Tournament/DeleteTournament/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateStatus(int id, int status)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"api/Tournament/UpdateStatus/{id}/{status}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> ValidAcceptAndRemoveTournament(int tourId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Tournament/ValidAcceptAndRemoveTournament/{tourId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(responseBody);
            return result;
        }

        public async Task<List<AttempDTO>> GetPlayers(int tourId, bool accepted)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Attemp/GetPlayers/{tourId}/{accepted}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<AttempDTO> result = JsonConvert.DeserializeObject<List<AttempDTO>>(responseBody);
            return result;
        }

        public async Task<bool> IsValidAcceptAndRemoveAttemp(int attempId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Attemp/IsValidAcceptAndRemoveAttemp/{attempId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(responseBody);
            return result;
        }

        public async Task<bool> ValidCalXp(int tourId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Attemp/ValidCalXp/{tourId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(responseBody);
            return result;
        }

        public async Task CalXp(int tourId)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"api/Attemp/CalXp/{tourId}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAcceptAttemp(int attempId, bool accepted)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"api/Attemp/UpdateAcceptAttemp/{attempId}/{accepted}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<AttempDTO> GetAttempById(int attempId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Attemp/GetAttempById/{attempId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            AttempDTO result = JsonConvert.DeserializeObject<AttempDTO>(responseBody);
            return result;
        }

        public async Task AddPlayers(int tourId, List<int> playerId)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Attemp/AddPlayers/{tourId}", playerId);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<UserDTO>> GetPlayers(string term)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetPlayers/{term}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<UserDTO> result = JsonConvert.DeserializeObject<List<UserDTO>>(responseBody);
            return result;
        }
        public async Task<List<UserDTO>> GetUsers(int role)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUsers/{role}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<UserDTO> result = JsonConvert.DeserializeObject<List<UserDTO>>(responseBody);
            return result;
        }

        public async Task CreateRound(RoundDTO round)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Round/CreateRound/", round);
            response.EnsureSuccessStatusCode();
        }

        public async Task<UserDTO> GetStatistic(UserDTO user)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/User/GetStatistic", user);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            UserDTO result = JsonConvert.DeserializeObject<UserDTO>(responseBody);
            return result;
        }

        public async Task<List<TournamentDTO>> GetUpComingTournament(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUpComingTournament/{userId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<TournamentDTO> result = JsonConvert.DeserializeObject<List<TournamentDTO>>(responseBody);
            return result;
        }

        public async Task<List<TournamentDTO>> GetRegisteredTournament(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetRegisteredTournament/{userId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<TournamentDTO> result = JsonConvert.DeserializeObject<List<TournamentDTO>>(responseBody);
            return result;
        }

        public async Task<List<TournamentDTO>> GetEndTournament(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetEndTournament/{userId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<TournamentDTO> result = JsonConvert.DeserializeObject<List<TournamentDTO>>(responseBody);
            return result;
        }

        public async Task<bool> CheckValidRequest(int tourId, int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Attemp/CkeckValidRequest/{tourId}/{userId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            bool result = JsonConvert.DeserializeObject<bool>(responseBody);
            return result;
        }

        public async Task CreateRequest(int tourId, int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Attemp/CreateRequest/{tourId}/{userId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
