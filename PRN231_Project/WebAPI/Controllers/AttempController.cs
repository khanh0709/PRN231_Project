using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Business.IRepository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttempController : ControllerBase
    {
        private IAttempRepository Repository;

        public AttempController(IAttempRepository Repository)
        {
            this.Repository = Repository;
        }
        
        [HttpGet("GetAttempById/{attempId}")]
        public IActionResult GetAttempById(int attempId)
        {
            return Ok(Repository.GetAttempById(attempId));
        }

        [HttpGet("GetPlayers/{tourId}/{accepted}")]
        public IActionResult GetPlayers(int tourId, bool accepted)
        {
            return Ok(Repository.GetPlayers(tourId, accepted));
        }

        [HttpGet("IsValidAcceptAndRemoveAttemp/{attempId}")]
        public IActionResult IsValidAcceptAndRemoveAttemp(int attempId)
        {
            return Ok(Repository.IsValidAcceptAndRemoveAttemp(attempId));
        }

        [HttpGet("ValidCalXp/{tourId}")]
        public IActionResult ValidCalXp(int tourId)
        {
            return Ok(Repository.ValidCalXp(tourId));
        }
        
        [HttpPut("CalXp/{tourId}")]
        public IActionResult CalXp(int tourId)
        {
            Repository.CalXp(tourId);
            return Ok();
        }

        [HttpPut("UpdateAcceptAttemp/{attempId}/{accepted}")]
        public IActionResult UpdateAcceptAttemp(int attempId, bool accepted)
        {
            Repository.UpdateAcceptAttemp(attempId, accepted);
            return Ok();
        }

        [HttpPost("AddPlayers/{tourId}")]
        public IActionResult AddPlayers(int tourId, List<int> playerId)
        {
            Repository.AddPlayers(tourId, playerId);
            return Ok();
        }
    }
}
