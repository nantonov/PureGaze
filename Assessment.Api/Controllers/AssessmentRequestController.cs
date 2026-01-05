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
}
