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
    IEmailFactory emailFactory) : IAssessmentRequestService
{
    public async Task<int> AppointAsync(AppointAssessmentDto dto, CancellationToken cancellationToken)
    {
        var m1 = await employeeRepository.GetEmployeeAsync(dto.M1Id, cancellationToken);
        var employee = await employeeRepository.GetEmployeeAsync(dto.EmployeeId, cancellationToken);

        if (m1 == null)
            throw new KeyNotFoundException($"M1 with Id {dto.M1Id} not found.");
        if (employee == null)
            throw new KeyNotFoundException($"Employee with Id {dto.EmployeeId} not found.");
        if (string.IsNullOrWhiteSpace(m1.Email))
            throw new ValidationException($"Manager {m1.FirstNameEn} {m1.LastNameEn} does not have a valid email address.");

        var assessmentRequest = new AssessmentRequest
        {
            EmployeeId = dto.EmployeeId,
            M1Id = dto.M1Id,
            M3Id = dto.M3Id,
            CodeId = dto.CodeId,
            RequestedToDate = dto.RequestedToDate,
            Status = AssessmentRequestStatus.Created
        };

        var email = emailFactory.CreateAssessmentRequestEmail(m1.Email, $"{employee.FirstNameEn} {employee.LastNameEn}");

        await emailRepository.AddAsync(email, cancellationToken);
        await assessmentRequestRepository.AddAsync(assessmentRequest, cancellationToken);
        await assessmentRequestRepository.SaveChangesAsync(cancellationToken);

        return assessmentRequest.Id;
    }
}
