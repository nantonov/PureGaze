using System.ComponentModel.DataAnnotations;
using Assessment.Application.Abstractions.Infrastructure;
using Assessment.Application.Abstractions.Services;
using Assessment.Application.Contracts.Application;
using Assessment.Application.Extensions;
using Common.Data.Enums;
using Common.Domain.Entities;

namespace Assessment.Application.Services;

public class AssessmentRequestService(
    IAssessmentRequestRepository assessmentRequestRepository, 
    IEmployeeRepository employeeRepository, 
    IEmailRepository emailRepository,
    ICodeRepository codeRepository,
    IEmailFactory emailFactory) : IAssessmentRequestService
{
    public async Task<int> AppointAsync(AppointAssessmentRequest request, CancellationToken ct)
    {
        var employee = await employeeRepository.GetEmployeeAsync(request.EmployeeId, ct);
        if (employee == null)
            throw new KeyNotFoundException($"Employee with Id {request.EmployeeId} not found.");

        var manager = employee.M1 ?? employee.M3;
            
        if (manager == null)
            throw new KeyNotFoundException($"Manager with Id {request.EmployeeId} not found.");
        
        if (employee.ProfessionalLevelId == null)
            throw new ValidationException($"Current Professional Level for Employee with Id {request.EmployeeId} is not set.");
        
        var codeId = await codeRepository.GetCodeIdByProfessionalLevelIdAsync(employee.ProfessionalLevelId.Value, ct);

        var assessmentRequest = new AssessmentRequest
        {
            EmployeeId = request.EmployeeId,
            ManagerId = manager.Id,
            CodeId = codeId,
            Status = AssessmentRequestStatus.Created
        };

        var email = emailFactory.CreateAssessmentRequestEmail(manager.Email!, $"{employee.FirstNameEn} {employee.LastNameEn}");

        //TODO: think about right saving
        await emailRepository.AddAsync(email, ct);
        await assessmentRequestRepository.AddAsync(assessmentRequest, ct);
        await assessmentRequestRepository.SaveChangesAsync(ct);

        return assessmentRequest.Id;
    }

    public async Task<AssessmentRequestDetailsDto> GetDetailsAsync(int assessmentRequestId, CancellationToken ct)
    {
        var assessmentRequest = await assessmentRequestRepository.GetByIdAsync(assessmentRequestId, ct)
            ?? throw new KeyNotFoundException($"Assessment Request with Id {assessmentRequestId} not found.");
        
        return assessmentRequest.ToDto();
    }

    public async Task<IReadOnlyList<AssessmentRequestDetailsDto>> GetMyAssessmentsAsync(int employeeId, int page, int pageSize, CancellationToken ct)
    {
        var items = 
            await assessmentRequestRepository.GetByEmployeeIdAsync(employeeId, page, pageSize, ct);
        
        return [.. items.Select(x => x.ToDto())];
    }

    public async Task<IReadOnlyList<AssessmentRequestDetailsDto>> GetAssignedAssessmentsAsync(int managerId, int page, int pageSize, CancellationToken ct)
    {
        var items = 
            await assessmentRequestRepository.GetByManagerIdAsync(managerId, page, pageSize, ct);
        
        return [.. items.Select(x => x.ToDto())];
    }
}
