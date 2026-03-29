using PureGaze.Application.Contracts.Application;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.Extensions;

public static class AssessmentRequestExtensions
{
    public static AssessmentRequestDetailsDto ToDetailsDto(this AssessmentRequest assessmentRequest)
        => new()
        {
            Id = assessmentRequest.Id,
            EmployeeId = assessmentRequest.EmployeeId,
            EmployeeFullName =
                $"{assessmentRequest.Employee?.FirstNameEn} {assessmentRequest.Employee?.LastNameEn}".Trim(),
            ManagerId = assessmentRequest.ManagerId,
            ManagerFullName = $"{assessmentRequest.Manager?.FirstNameEn} {assessmentRequest.Manager?.LastNameEn}".Trim(),
            Code = assessmentRequest.Code?.Name,
            Status = assessmentRequest.Status,
            RejectionReason = assessmentRequest.RejectionReason
        };

    public static AssessmentRequestDto ToDto(this AssessmentRequest assessmentRequest)
        => new()
        {
            Id = assessmentRequest.Id,
            EmployeeFullName =
                $"{assessmentRequest.Employee?.FirstNameEn} {assessmentRequest.Employee?.LastNameEn}".Trim(),
            ManagerFullName = $"{assessmentRequest.Manager?.FirstNameEn} {assessmentRequest.Manager?.LastNameEn}".Trim(),
            Code = assessmentRequest.Code?.Name,
            Status = assessmentRequest.Status,
        };

    public static Assessment ToAssessment(this AssessmentRequest request, Template template)
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