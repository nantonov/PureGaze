using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using System.ComponentModel.DataAnnotations;
using PureGaze.Domain.Enums;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.AssignAssessmentStage;

public sealed class AssignAssessmentStageHandler(
    IEmployeeRepository employeeRepository,
    IAssessmentRepository assessmentRepository,
    IAssessmentStageRepository assessmentStageRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<AssignAssessmentStageCommand>
{
    public async Task Handle(AssignAssessmentStageCommand request, CancellationToken ct)
    {
        AssessmentStage assessmentStage = await assessmentStageRepository.GetByIdAsync(request.AssessmentStageId, ct)
            ?? throw new KeyNotFoundException($"Assessment stage with Id {request.AssessmentStageId} not found.");
        if (assessmentStage.AssessorId != null)
            throw new ValidationException($"Assessment stage with Id {request.AssessmentStageId} already has an assessor assigned.");

        string managerEmail = currentUserContextProvider.GetUserEmail();
        Employee manager = await employeeRepository.GetByEmailAsync(managerEmail, ct)
            ?? throw new KeyNotFoundException($"Manager with email {managerEmail} was not found.");
        if (manager.ProfessionalLevel == null)
            throw new ValidationException($"Current Professional Level for Manager with Id {manager.Id} is not set.");

        int targetGradeOrder = assessmentStage.Assessment?.Code?.ToGrade?.OrderValue
            ?? throw new ValidationException("Assessment target grade is not set.");

        if (manager.ProfessionalLevel.OrderValue <= targetGradeOrder)
            throw new ValidationException("Professional Level for Manager in not enough for this assigment.");

        Employee employee = await employeeRepository.GetByIdAsync(assessmentStage.Assessment!.EmployeeId, ct)
            ?? throw new KeyNotFoundException($"Employee with Id {assessmentStage.Assessment.EmployeeId} not found.");

        bool alreadyAssigned = await assessmentStageRepository
            .HasAssessorInAssessmentAsync(assessmentStage.AssessmentId, manager.Id, ct);
        if (alreadyAssigned)
            throw new ValidationException("You are already assigned to a topic in this assessment.");

        assessmentStage.AssessorId = manager.Id;
        await assessmentStageRepository.SaveChangesAsync(ct);

        Assessment? assessment = await assessmentRepository.GetByIdAsync(assessmentStage.AssessmentId, ct);
        if (assessment!.Stages.All(s => s.AssessorId != null))
        {
            assessment.Status = AssessmentStatus.InProgress;
            await assessmentRepository.SaveChangesAsync(ct);
        }

        string? topicName = assessmentStage.Topic?.TopicTranslates
            .FirstOrDefault(x => x.Language == Language.En)?.Name;
        Email email = emailFactory.CreateAssessmentStageAssignEmail(
            employee?.Email ?? "",
            $"{manager.FirstNameEn} {manager.LastNameEn}",
            topicName ?? "");
        await emailRepository.AddAsync(email, ct);
        await emailRepository.SaveChangesAsync(ct);
    }
}