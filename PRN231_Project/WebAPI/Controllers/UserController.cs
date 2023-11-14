using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Business.DTO;
using WebAPI.Business.Enums;
using WebAPI.Business.IRepository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings AppSettings;
        private IUserRepository UserRepository;
        private ITournamentRepository TournamentRepository;

        public UserController(IOptionsMonitor<AppSettings> optionsMonitor, IUserRepository UserRepository, ITournamentRepository TournamentRepository)
        {
            this.AppSettings = optionsMonitor.CurrentValue;
            this.UserRepository = UserRepository;
            this.TournamentRepository = TournamentRepository;
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

        [Authorize(Roles = "Admin, Player")]

        [HttpPut("EditProfile")]
        public IActionResult EditProfile(UpdateProfileDTO profile)
        {

            UserDTO u = UserRepository.GetUserById(profile.id);
            if (u == null)
                return NotFound();

            //if (BCrypt.Net.BCrypt.Verify(oldPass, u.Password))
            if (!string.IsNullOrEmpty(profile.oldPass))
            {

                if (profile.oldPass != u.Password)        //nhớ đổi thành đoạn verify bcrypt bên trên
                {
                    return BadRequest();
                }
                u.Password = profile.newPass;           //nhớ đổi thành đoạn encrypt bên trên
            }
            u.FullName = profile.name;
            u.Email = profile.email;
            u.City = profile.city;

            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPass, salt);
            //u.Password = hashedPassword;

            UserRepository.UpdateUser(u);
            return NoContent();
        }

        [HttpGet("GetUsersById/{id}")]
        public IActionResult GetUsersById(int id)
        {
            var user = UserRepository.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("GetUsersByAcc/{acc}")]
        public IActionResult GetUsersByAcc(string acc)
        {
            var user = UserRepository.GetUserByAcc(acc);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("AddPlayer")]
        public IActionResult AddPlayer(UserDTO user)
        {
            try
            {
                //string salt = BCrypt.Net.BCrypt.GenerateSalt();
                //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                //user.Password = hashedPassword;
                UserRepository.AddPlayer(user);                 //nhớ đổi thành đoạn encrypt
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost("GetStatistic")]
        public IActionResult GetStatistic(UserDTO user)
        {
            UserDTO u = UserRepository.GetStatistic(user);
            return Ok(u);
        }

        [HttpGet("GetUpComingTournament/{userId}")]
        public IActionResult GetUpComingTournament(int userId)
        {
            List<TournamentDTO> list = TournamentRepository.GetUpComingTournament(userId);
            return Ok(list);
        }

        [HttpGet("GetRegisteredTournament/{userId}")]
        public IActionResult GetRegisteredTournament(int userId)
        {
            List<TournamentDTO> list = TournamentRepository.GetRegisteredTournament(userId);
            return Ok(list);
        }

        [HttpGet("GetEndTournament/{userId}")]
        public IActionResult GetEndTournament(int userId)
        {
            List<TournamentDTO> list = TournamentRepository.GetEndTournament(userId);
            return Ok(list);
        }

    }
}
