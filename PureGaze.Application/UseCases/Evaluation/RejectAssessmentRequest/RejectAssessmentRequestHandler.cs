using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.RejectAssessmentRequest;

public class RejectAssessmentRequestHandler(
    IAssessmentRequestRepository assessmentRequestRepository)
    : IRequestHandler<RejectAssessmentRequestCommand>
{
    public async Task Handle(RejectAssessmentRequestCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(command.Reason))
            throw new ArgumentException("Rejection reason cannot be empty.");
        
        var request = await assessmentRequestRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Assessment request with Id {command.Reason} not found.");
        
        request.Status = AssessmentRequestStatus.Rejected;
        request.RejectionReason = command.Reason;
        await assessmentRequestRepository.SaveChangesAsync(ct);
    }
}
