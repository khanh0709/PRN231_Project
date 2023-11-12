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
    public class TypeController : ControllerBase
    {
        private ITypeRepository Repository;

        public TypeController(ITypeRepository Repository)
        {
            this.Repository = Repository;
        }

        [HttpGet("GetTypes")]
        public IActionResult GetTypes()
        {
            return Ok(Repository.GetTypes());
        }
    }
}
