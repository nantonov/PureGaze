using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.ApproveAssessmentRequest;

public class ApproveAssessmentRequestHandler(
    IAssessmentRequestRepository assessmentRequestRepository,
    IAssessmentRepository assessmentRepository,
    ITemplateRepository templateRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository)
    : IRequestHandler<ApproveAssessmentRequestCommand>
{
    public async Task Handle(ApproveAssessmentRequestCommand command, CancellationToken ct = default)
    {
        var request = await assessmentRequestRepository.GetByIdWithEmployeeAsync(command.Id, ct)
                      ?? throw new KeyNotFoundException($"Assessment request with Id {command.Id} not found.");
        
        if (request.Status == AssessmentRequestStatus.Approved) 
            throw new InvalidOperationException("Request is already approved.");
        
        var template = await templateRepository.GetByCodeIdAsync(request.CodeId, ct)
            ?? throw new KeyNotFoundException($"Template for Code {request.CodeId} not found.");

        request.Status = AssessmentRequestStatus.Approved;
        
        var email = emailFactory.CreateAssessmentApprovedEmail(
            request.Employee?.Email!, $"{request.Employee?.FirstNameEn} {request.Employee?.LastNameEn}");

        await assessmentRepository.AddAsync(request.ToAssessment(template), ct);
        await emailRepository.AddAsync(email, ct);
        await assessmentRequestRepository.SaveChangesAsync(ct);
    }
}
