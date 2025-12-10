using Diagnost.Domain;
using Diagnost.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Diagnost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessCodeController : ControllerBase
    {
        private readonly IAccessCodeService _accessCode;

        public AccessCodeController(IAccessCodeService accessCode)
        {
            _accessCode = accessCode;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            (Error error, List<AccessCode>? accessCodes) = await _accessCode.GetAsync();
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(accessCodes);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult> Get([FromRoute] string code)
        {
            (Error error, AccessCode? accessCode) = await _accessCode.GetAsync(code);
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(accessCode);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            (Error error, AccessCode? accessCode) = await _accessCode.CreateAsync();
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }

            return Ok(accessCode);
        }

        [HttpPut("{code}")]
        public async Task<ActionResult> Put([FromRoute] string code)
        {
            (Error error, AccessCode? accessCode) = await _accessCode.Revoke(code);
            if (error.IsError)
            {
                BadRequest(error.Message);
            }
            return Ok(accessCode);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete([FromRoute] string code)
        {
            (Error error, AccessCode? accessCode) = await _accessCode.DeleteAsync(code);
            if (error.IsError)
            {
                BadRequest(error.Message);
            }
            return Ok(accessCode);
        }
    }
}
