using WebAPI.Business.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Business.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Business.Enums;
using WebAPI.DataAccess.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings AppSettings;
        private IUserRepository UserRepository;

        public UserController(IOptionsMonitor<AppSettings> optionsMonitor, IUserRepository UserRepository)
        {
            this.AppSettings = optionsMonitor.CurrentValue;
            this.UserRepository = UserRepository;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO user)
        {
            UserDTO userDTO = UserRepository.GetUserByAccAndPass(user.Account, user.Password);
            if (userDTO == null)
            {
                return NotFound();
            }
            else
            {
                userDTO.Token = GenerateToken(userDTO);
                return Ok(userDTO);
            }
        }

        [HttpGet("GetPlayers/{term?}")]
        public IActionResult GetPlayers(string? term)
        {
            var results = UserRepository.GetPlayers(term)
            //    .Select(user => new
            //{
            //    Id = user.UserId,
            //    Text = user.FullName + " (" + user.UserId + ")"
            //})
                ;
            return Ok(results); 
        }

        [HttpGet("GetUsers/{term}")]
        public IActionResult GetUsers(int role)
        {
            return Ok(UserRepository.GetUsers(role));
        }

        [HttpGet("GetPlayersInTournament/{tourId}/{term?}")]
        public IActionResult GetPlayersInTournament(int tourId, string? term)
        {
            return Ok(UserRepository.GetPlayersInTournament(tourId, term));
        }

        [HttpGet("GetRanking/{city?}/{term?}")]
        public IActionResult GetRanking(string? city, string? term)
        {
            if (city == "0") city = null;
            return Ok(UserRepository.GetRanking(city, term));
        }

        private string GenerateToken(UserDTO user)
        {
            var jwtTokenHandle = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(AppSettings.SecretKey);
            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Role, ((UserRole)user.Role).ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandle.CreateToken(tokenDes);
            return jwtTokenHandle.WriteToken(token);
        }
    }
}
