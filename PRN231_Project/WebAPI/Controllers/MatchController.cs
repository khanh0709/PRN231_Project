using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Business.DTO;
using WebAPI.Business.IRepository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private IMatchRepository Repository;

        public MatchController(IMatchRepository Repository)
        {
            this.Repository = Repository;
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("GetMatchesByRoundId/{roundId}")]
        public IActionResult GetMatchesByRoundId(int roundId)
        {
            return Ok(Repository.GetMatchesByRoundId(roundId));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("SaveMatches")]
        public IActionResult SaveMatches(SaveMatchDTO model)
        {
            Repository.SaveMatches(model.roundId, model.player1Id, model.player2Id, model.winerId);
            return Ok();
        }
    }
}
