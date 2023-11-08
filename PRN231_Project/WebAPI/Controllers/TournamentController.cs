using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private ITournamentRepository TournamentRepository;

        public TournamentController(ITournamentRepository TournamentRepository)
        {
            this.TournamentRepository = TournamentRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetTournamentsByUser/{userId}")]
        public IActionResult GetTournamentsByUser(int userId)
        {
            return Ok(TournamentRepository.GetTournamentsByUser(userId));
        }
    }
}
