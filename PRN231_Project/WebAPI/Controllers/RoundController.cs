using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;
using WebAPI.Business.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundController : ControllerBase
    {
        private IRoundRepository Repository;

        public RoundController(IRoundRepository Repository)
        {
            this.Repository = Repository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateRound")]
        public IActionResult CreateRound(RoundDTO round)
        {
            Repository.CreateRound(round);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetRoundByTournamentId/{tourId}")]
        public IActionResult GetRoundByTournamentId(int tourId)
        {
            return Ok(Repository.GetRoundByTournamentId(tourId));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteRound/{roundId}")]
        public IActionResult DeleteRound(int roundId)
        {
            Repository.DeleteRound(roundId);
            return Ok();
        }
    }
}
