using System.ComponentModel.DataAnnotations;
using Assessment.Application.Abstractions.Infrastructure;
using Assessment.Application.Abstractions.Services;
using Assessment.Application.Contracts.Application;
using Common.Data.Enums;
using Common.Domain.Entities;
using Notification.Application.Abstractions.Infrastructure;

namespace Assessment.Application.Services;

public class AssessmentRequestService(
    IAssessmentRequestRepository assessmentRequestRepository, 
    IEmployeeRepository employeeRepository, 
    IEmailRepository emailRepository,
    ICodeRepository codeRepository,
    IEmailFactory emailFactory) : IAssessmentRequestService
{
    public async Task<int> AppointAsync(AppointAssessmentRequest request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetEmployeeAsync(request.EmployeeId, cancellationToken);
        if (employee == null)
            throw new KeyNotFoundException($"Employee with Id {request.EmployeeId} not found.");

        var manager = employee.M1 ?? employee.M3;
            
        if (manager == null)
            throw new KeyNotFoundException($"Manager with Id {request.EmployeeId} not found.");
        
        if (employee.ProfessionalLevelId == null)
            throw new ValidationException($"Current Professional Level for Employee with Id {request.EmployeeId} is not set.");
        
        var codeId = await codeRepository.GetCodeIdByProfessionalLevelIdAsync(employee.ProfessionalLevelId.Value, cancellationToken);

        var assessmentRequest = new AssessmentRequest
        {
            EmployeeId = request.EmployeeId,
            ManagerId = manager.Id,
            CodeId = codeId,
            Status = AssessmentRequestStatus.Created
        };

        var email = emailFactory.CreateAssessmentRequestEmail(manager.Email!, $"{employee.FirstNameEn} {employee.LastNameEn}");

        //TODO: think about right saving
        await emailRepository.AddAsync(email, cancellationToken);
        await assessmentRequestRepository.AddAsync(assessmentRequest, cancellationToken);
        await assessmentRequestRepository.SaveChangesAsync(cancellationToken);

        return assessmentRequest.Id;
    }
}
