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

        [Authorize(Roles = "Admin")]
        [HttpGet("GetTournamentsByIdAndUser/{tourId}/{userId}")]
        public IActionResult GetTournamentsByIdAndUser(int tourId, int userId)
        {
            return Ok(TournamentRepository.GetTournamentsByIdAndUser(tourId, userId));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ValidAcceptAndRemoveTournament/{tourId}")]
        public IActionResult ValidAcceptAndRemoveTournament(int tourId)
        {
            return Ok(TournamentRepository.ValidAcceptAndRemoveTournament(tourId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateTournament")]
        public IActionResult CreateTournament(TournamentDTO tour)
        {
            TournamentRepository.CreateTournament(tour);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateInfoTournament")]
        public IActionResult UpdateInfoTournament(TournamentDTO tour)
        {
            var tournament = TournamentRepository.GetTournamentById(tour.TournamentId);
            if(tournament != null) 
            {
                TournamentRepository.UpdateInfoTournament(tour);
                return NoContent();
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteTournament/{id}")]
        public IActionResult DeleteTournament(int id)
        {
            var tournament = TournamentRepository.GetTournamentById(id);
            if (tournament != null)
            {
                TournamentRepository.DeleteTournament(id);
                return NoContent();
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateStatus/{id}/{status}")]
        public IActionResult UpdateStatus(int id, int status)
        {
            var tournament = TournamentRepository.GetTournamentById(id);
            if (tournament != null)
            {
                TournamentRepository.UpdateStatus(id, status);
                return NoContent();
            }
            return NotFound();
        }

        
    }
}
