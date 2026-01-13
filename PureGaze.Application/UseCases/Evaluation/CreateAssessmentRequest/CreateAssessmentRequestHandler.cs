using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.CreateAssessmentRequest;

public class CreateAssessmentRequestHandler(
    IEmployeeRepository employeeRepository,
    ICodeRepository codeRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository,
    IAssessmentRequestRepository assessmentRequestRepository)
    : IRequestHandler<CreateAssessmentRequestCommand>
{
    public async Task Handle(CreateAssessmentRequestCommand assessmentRequestCommand, CancellationToken ct = default)
    {
        var employee = await employeeRepository.GetByIdAsync(assessmentRequestCommand.EmployeeId, ct)
            ?? throw new KeyNotFoundException($"Employee with Id {assessmentRequestCommand.EmployeeId} not found.");
        
        var manager = 
            employee.M1 
            ?? employee.M3 
            ?? throw new KeyNotFoundException($"Manager with Id {assessmentRequestCommand.EmployeeId} not found.");
        
        if (employee.ProfessionalLevelId == null)
            throw new ValidationException($"Current Professional Level for Employee with Id {assessmentRequestCommand.EmployeeId} is not set.");
        
        var code = await codeRepository.GetByProfessionalLevelIdAsync(employee.ProfessionalLevelId.Value, ct)
            ?? throw new KeyNotFoundException($"Code for Employee with Id {assessmentRequestCommand.EmployeeId} not found.");
        
        await assessmentRequestRepository.AddAsync(new AssessmentRequest
        {
            EmployeeId = assessmentRequestCommand.EmployeeId,
            ManagerId = manager.Id,
            CodeId = code?.Id ?? 0,
            Status = AssessmentRequestStatus.Created
        }, ct);
        
        await assessmentRequestRepository.SaveChangesAsync(ct);
        
        await emailRepository.AddAsync(
            emailFactory.CreateAssessmentRequestEmail( 
                manager.Email!, 
                $"{employee.FirstNameEn} {employee.LastNameEn}"), 
            ct);
        
        await emailRepository.SaveChangesAsync(ct);
    }
}