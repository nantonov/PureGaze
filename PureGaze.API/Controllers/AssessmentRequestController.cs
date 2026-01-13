using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.CreateAssessmentRequest;
using PureGaze.Application.UseCases.Evaluation.GetAssessmentRequestDetails;
using PureGaze.Application.UseCases.Evaluation.GetAssignetToMeRequests;
using PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("assessment-requests")]
public class AssessmentRequestController(IRequestDispatcher dispatcher) 
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new CreateAssessmentRequestCommand(1), ct);

        return Ok();
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetDetails([FromRoute] int id, CancellationToken ct = default)
    {
        var result = 
            await dispatcher
                .SendAsync<GetAssessmentRequestDetailsQuery, GetAssessmentRequestDetailsResponse>(
                    new GetAssessmentRequestDetailsQuery(id), ct);
        
        return Ok(result);
    }
    
    [HttpGet("my")]
    public async Task<IActionResult> GetMy(GetMyAssessmentRequestsQuery query, CancellationToken ct = default)
    {
        var result =
            await dispatcher
                .SendAsync<GetMyAssessmentRequestsQuery, GetMyAssessmentRequestsResult>(query, ct);
    
        return Ok(result);
    }
    
    [HttpGet("assigned-to-me")]
    public async Task<IActionResult> GetAssignedToMe(GetAssignedToMeRequestsQuery query, CancellationToken ct = default)
    {
        var result =
            await dispatcher
                .SendAsync<GetAssignedToMeRequestsQuery, GetAssignedToMeRequestsResult>(query, ct);
    
        return Ok(result);
    }
}
