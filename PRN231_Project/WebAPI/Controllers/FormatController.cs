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
    public class FormatController : ControllerBase
    {
        private IFormatRepository Repository;

        public FormatController(IFormatRepository Repository)
        {
            this.Repository = Repository;
        }

        [HttpGet("GetFormats")]
        public IActionResult GetFormats()
        {
            return Ok(Repository.GetFormats());
        }
    }
}
