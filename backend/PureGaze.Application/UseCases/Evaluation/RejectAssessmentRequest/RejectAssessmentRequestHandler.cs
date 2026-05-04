using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.RejectAssessmentRequest;

public class RejectAssessmentRequestHandler(
    IAssessmentRequestRepository assessmentRequestRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository,
    IEmailQueuePublisher? queuePublisher = null)
    : IRequestHandler<RejectAssessmentRequestCommand>
{
    public async Task Handle(RejectAssessmentRequestCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(command.Reason))
            throw new ArgumentException("Rejection reason cannot be empty.");

        var request = await assessmentRequestRepository.GetByIdWithEmployeeAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Assessment request with Id {command.Id} not found.");

        request.Status = AssessmentRequestStatus.Rejected;
        request.RejectionReason = command.Reason;

        var email = emailFactory.CreateAssessmentRejectedEmail(
            request.Employee?.Email!, $"{request.Employee?.FirstNameEn} {request.Employee?.LastNameEn}", command.Reason);

        await emailRepository.AddAsync(email, ct);
        await assessmentRequestRepository.SaveChangesAsync(ct);

        if (queuePublisher is not null)
            await queuePublisher.PublishAsync(email.Id, ct);
    }
}
