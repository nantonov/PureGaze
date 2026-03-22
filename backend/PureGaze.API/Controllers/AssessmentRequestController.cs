using Microsoft.AspNetCore.Mvc;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Evaluation.ApproveAssessmentRequest;
using PureGaze.Application.UseCases.Evaluation.CreateAssessmentRequest;
using PureGaze.Application.UseCases.Evaluation.GetAssessmentRequestDetails;
using PureGaze.Application.UseCases.Evaluation.GetAssignedToMeRequests;
using PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;
using PureGaze.Application.UseCases.Evaluation.RejectAssessmentRequest;

namespace PureGaze.API.Controllers;

[ApiController]
[Route("assessment-requests")]
public class AssessmentRequestController(IRequestDispatcher dispatcher) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create(CancellationToken ct = default)
    {
        await dispatcher.SendAsync(new CreateAssessmentRequestCommand(), ct);

        return Ok();
    }

    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] ApproveAssessmentRequestCommand command,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);

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

    [HttpPost("my")]
    public async Task<IActionResult> GetMy([FromBody]GetMyAssessmentRequestsQuery query, CancellationToken ct = default)
    {
        var result =
            await dispatcher
                .SendAsync<GetMyAssessmentRequestsQuery, GetMyAssessmentRequestsResult>(query, ct);

        return Ok(result);
    }

    [HttpPost("assigned-to-me")]
    public async Task<IActionResult> GetAssignedToMe([FromBody]GetAssignedToMeRequestsQuery query, CancellationToken ct = default)
    {
        var result =
            await dispatcher
                .SendAsync<GetAssignedToMeRequestsQuery, GetAssignedToMeRequestsResult>(query, ct);

        return Ok(result);
    }

    [HttpPost("reject")]
    public async Task<IActionResult> Reject([FromBody] RejectAssessmentRequestCommand command,
        CancellationToken ct = default)
    {
        await dispatcher.SendAsync(command, ct);

        return Ok();
    }
}