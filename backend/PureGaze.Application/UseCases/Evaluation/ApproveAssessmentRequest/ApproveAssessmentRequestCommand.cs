using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.ApproveAssessmentRequest;

public sealed record ApproveAssessmentRequestCommand(int Id) : IRequest
{
    public static Assessment ToEntity(AssessmentRequest request, Template template)
        => new()
        {
            EmployeeId = request.EmployeeId,
            CodeId = request.CodeId,
            TemplateId = template.Id,
            Status = AssessmentStatus.Created,
            Stages = [.. template.Topics.Select(topic => new AssessmentStage
            {
                TopicId = topic.Id,
                Status = StageStatus.Pending
            })]
        };
}
