using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using System.ComponentModel.DataAnnotations;
using PureGaze.Domain.Enums;
using PureGaze.Application.Abstractions.Providers;

namespace PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;

public sealed class AssignAssessmentStageHandler(
    IEmployeeRepository employeeRepository,
    IAssessmentStageRepository assessmentStageRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository,
    ICurrentUserContextProvider currentUserContextProvider) 
    : IRequestHandler<AssignAssessmentStageCommand>
{
    public async Task Handle(AssignAssessmentStageCommand request, CancellationToken ct)
    {
        var assessmentStage = await assessmentStageRepository.GetByIdAsync(request.AssessmentStageId, ct)
            ?? throw new KeyNotFoundException($"Assessment stage with Id {request.AssessmentStageId} not found.");
        if (assessmentStage.AssessorId != null)
            throw new ValidationException($"Assessment stage with Id {request.AssessmentStageId} already has an assessor assigned.");

        var managerEmail = currentUserContextProvider.GetUserEmail();
        var manager = await employeeRepository.GetByEmailAsync(managerEmail, ct)
            ?? throw new KeyNotFoundException($"Manager with email {managerEmail} was not found.");
        if (manager.ProfessionalLevel == null)
            throw new ValidationException($"Current Professional Level for Manager with Id {manager.Id} is not set.");

        var employee = await employeeRepository.GetByIdAsync(assessmentStage.Assessment.EmployeeId, ct)
            ?? throw new KeyNotFoundException($"Employee with Id {assessmentStage.Assessment.EmployeeId} not found.");
        if (employee.ProfessionalLevel == null)
            throw new ValidationException($"Current Professional Level for Employee with Id {assessmentStage.Assessment.EmployeeId} is not set.");

        if (manager.ProfessionalLevel.OrderValue <= employee.ProfessionalLevel.OrderValue)
            throw new ValidationException("Professional Level for Manager in not enought for this assigment");

        assessmentStage.AssessorId = manager.Id;
        await assessmentStageRepository.SaveChangesAsync(ct);
        
        var topicName = assessmentStage.Topic.TopicTranslates
            .FirstOrDefault(x => x.Language == Language.English)?.Name;
        var email = emailFactory.CreateAssessmentStageAssignEmail(
            employee?.Email ?? "", 
            $"{manager.FirstNameEn} {manager.LastNameEn}",
            topicName ?? "");
        await emailRepository.AddAsync(email, ct);
        await emailRepository.SaveChangesAsync(ct);
    }
}