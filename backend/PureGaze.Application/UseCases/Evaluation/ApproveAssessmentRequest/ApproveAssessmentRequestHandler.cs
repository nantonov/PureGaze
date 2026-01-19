using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.ApproveAssessmentRequest;

public class ApproveAssessmentRequestHandler(
    IAssessmentRequestRepository assessmentRequestRepository,
    IAssessmentRepository assessmentRepository,
    ITemplateRepository templateRepository)
    : IRequestHandler<ApproveAssessmentRequestCommand>
{
    public async Task Handle(ApproveAssessmentRequestCommand command, CancellationToken ct = default)
    {
        var request = await assessmentRequestRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Assessment request with Id {command.Id} not found.");

        request.Status = AssessmentRequestStatus.Approved;

        var template = await templateRepository.GetByCodeIdAsync(request.CodeId, ct)
            ?? throw new KeyNotFoundException($"Template for Code {request.CodeId} not found.");

        var assessment = ToDomain(request, template);

        await assessmentRepository.AddAsync(assessment, ct);

        await assessmentRequestRepository.SaveChangesAsync(ct);
    }

    private static Assessment ToDomain(AssessmentRequest request, Template template)
    {
        return new Assessment
        {
            EmployeeId = request.EmployeeId,
            CodeId = request.CodeId,
            TemplateId = template.Id,
            Status = AssessmentStatus.Created,
            Stages = template.Topics.Select(topic => new AssessmentStage
            {
                TopicId = topic.Id,
                Status = StageStatus.Pending
            }).ToList()
        };
    }
}
