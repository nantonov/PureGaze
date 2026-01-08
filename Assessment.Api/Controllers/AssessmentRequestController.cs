using Assessment.Application.Abstractions.Services;
using Assessment.Application.Contracts.Application;
using Microsoft.AspNetCore.Mvc;
    
namespace Assessment.Api.Controllers;

[ApiController]
[Route("assessment-requests")]
public class AssessmentRequestController(IAssessmentRequestService assessmentRequestService) 
    : ControllerBase
{
    [HttpPost]
    [Route("appoint")]
    public async Task<IActionResult> Appoint(CancellationToken ct)
    {
        await assessmentRequestService
            .AppointAsync(new AppointAssessmentRequest(1), ct);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken ct)
    {
        var result = await assessmentRequestService.GetDetailsAsync(id, ct);
        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMy([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var myId = 1;
        
        var result = 
            await assessmentRequestService.GetMyAssessmentsAsync(myId, page, pageSize, ct);

        return Ok(result);
    }

    [HttpGet("assigned-to-me")]
    public async Task<IActionResult> GetAssignedToMe([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var managerId = 1;
        var result = 
            await assessmentRequestService.GetAssignedAssessmentsAsync(managerId, page, pageSize, ct);

        return Ok(result);
    }
}
