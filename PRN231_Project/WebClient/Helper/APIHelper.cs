﻿using WebAPI.Business.DTO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using WebAPI.DataAccess.Models;

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
            if (!string.IsNullOrEmpty(_session.GetString("token")))
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

        public async Task<List<UserDTO>> GetPlayersInTournament(int tourId, string? term)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetPlayersInTournament/{tourId}/{term}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<UserDTO> result = JsonConvert.DeserializeObject<List<UserDTO>>(responseBody);
            return result;
        }

        public async Task<List<RoundDTO>> GetRoundByTournamentId(int tourId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Round/GetRoundByTournamentId/{tourId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<RoundDTO> result = JsonConvert.DeserializeObject<List<RoundDTO>>(responseBody);
            return result;
        }

        public async Task<List<MatchDTO>> GetMatchesByRoundId(int tourId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Match/GetMatchesByRoundId/{tourId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<MatchDTO> result = JsonConvert.DeserializeObject<List<MatchDTO>>(responseBody);
            return result;
        }

        public async Task SaveMatches(SaveMatchDTO model)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Match/SaveMatches", model);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task DeleteRound(int roundId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Round/DeleteRound/{roundId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<UserDTO>> GetRanking(string? city, string? term)
        {
            string url = "api/User/GetRanking";
            if (!string.IsNullOrEmpty(city))
                url += $"/{city}";
            else
                url += "/0";
            if (!string.IsNullOrEmpty(term)) 
                url += $"/{term}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<UserDTO> result = JsonConvert.DeserializeObject<List<UserDTO>>(responseBody);
            return result;
        }
    }
}
