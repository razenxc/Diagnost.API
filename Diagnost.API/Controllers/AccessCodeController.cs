using Diagnost.Domain;
using Diagnost.Domain.Models;
using Microsoft.AspNetCore.Mvc;

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
        
        // GET: api/<AccessCodeController>
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

        // GET api/<AccessCodeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccessCodeController>
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

        // PUT api/<AccessCodeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccessCodeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
