using Diagnost.API.Contracts;
using Diagnost.API.Mappers;
using Diagnost.Domain.Interfaces;
using Diagnost.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diagnost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ResultRequest request)
        {
            (Error error, Result? result) = await _resultService.CreateAsync(request.ToResult(), request.AccessCode);
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }

            return Ok(result.ToResultResponse(result.AccessCode.Code));
        }

        // Update PZMR
        [HttpPut("PZMR")]
        public async Task<ActionResult> PutPZMR([FromBody] PZMRResultRequest request)
        {
            (Error error, Result? result) = await _resultService.SaveTestAsync(request.ToPZMRREsult(), request.AccessCode, request.ResultId, ResultType.PZMR);
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(result.ToResultResponse(result.AccessCode.Code));
        }

        // Update PV2
        [HttpPut("PV2_3")]
        public async Task<ActionResult> PutPV2_3([FromBody] PV2_3ResultRequest request)
        {
            (Error error, Result? result) = await _resultService.SaveTestAsync(request.ToPV2_3Result(), request.AccessCode, request.ResultId, ResultType.PV2_3);
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(result.ToResultResponse(result.AccessCode.Code));
        }

        // Update UFP
        [HttpPut("UFP")]
        public async Task<ActionResult> PutUFP([FromBody] UFPResultRequest request)
        {
            (Error error, Result? result) = await _resultService.SaveTestAsync(request.ToUFPResult(), request.AccessCode, request.ResultId, ResultType.UFP);
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(result.ToResultResponse(result.AccessCode.Code));
        }

        // Read All
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            (Error error, List<Result>? results) = await _resultService.GetAsync();
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(results.Select(x => x.ToResultResponse(x.AccessCode.Code)));
        }

        // Read
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            (Error error, Result? result) = await _resultService.GetAsync(id);
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(result.ToResultResponse(result.AccessCode.Code));
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            (Error error, Result? result) = await _resultService.DeleteAsync(id);
            if (error.IsError)
            {
                return BadRequest(error.Message);
            }
            return Ok(result.ToResultResponse(result.AccessCode.Code));
        }
    }
}
