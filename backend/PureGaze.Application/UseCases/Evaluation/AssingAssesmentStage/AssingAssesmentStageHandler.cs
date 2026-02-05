using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Evaluation.AssingAssesmentStage;

public sealed class AssingAssesmentStageHandler(
    IEmployeeRepository employeeRepository,
    IAssessmentStageRepository assessmentStageRepository) : IRequestHandler<AssingAssesmentStageCommand>
{
    public async Task Handle(AssingAssesmentStageCommand request, CancellationToken ct)
    {
        var assessmentStage = await assessmentStageRepository.GetByIdAsync(request.AssessmentStageId)
            ?? throw new KeyNotFoundException($"Assesment stage with Id {request.AssessmentStageId} not found.");
        if (assessmentStage.AssessorId != null)
            throw new ValidationException($"Assesment stage with Id {request.AssessmentStageId} already has an assessor assigned.");

        var manager = await employeeRepository.GetByEmailAsync(request.ManagerEmail, ct)
            ?? throw new KeyNotFoundException($"Manager with Id {request.ManagerEmail} not found.");
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
    }
}