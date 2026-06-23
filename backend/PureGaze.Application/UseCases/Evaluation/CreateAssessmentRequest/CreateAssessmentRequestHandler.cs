using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.CreateAssessmentRequest;

public class CreateAssessmentRequestHandler(
    IEmployeeRepository employeeRepository,
    ICodeRepository codeRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository,
    IAssessmentRequestRepository assessmentRequestRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<CreateAssessmentRequestCommand>
{
    public async Task Handle(CreateAssessmentRequestCommand assessmentRequestCommand, CancellationToken ct = default)
    {
        string email = currentUserContextProvider.GetUserEmail();

        Employee employee = await employeeRepository.GetByEmailAsync(email, ct)
            ?? throw new KeyNotFoundException($"Employee with email {email} not found.");

        Employee manager =
            employee.M1
            ?? employee.M3
            ?? throw new KeyNotFoundException($"Manager for email {email} not found.");

        if (employee.ProfessionalLevelId == null)
            throw new ValidationException($"Current Professional Level for Employee with email {email} is not set.");

        Code code = await codeRepository.GetByProfessionalLevelIdAsync(employee.ProfessionalLevelId.Value, ct)
            ?? throw new KeyNotFoundException($"Code for Employee with email {email} not found.");

        AssessmentRequest? assessmentRequest = await assessmentRequestRepository.GetEmployeeActiveAssessmentRequest(employee.Id, ct);
        if (assessmentRequest != null)
            throw new Exception("You already have active assessment request.");

        await assessmentRequestRepository.AddAsync(new AssessmentRequest
        {
            EmployeeId = employee.Id,
            ManagerId = manager.Id,
            CodeId = code?.Id ?? 0,
            Status = AssessmentRequestStatus.Created
        }, ct);

        await assessmentRequestRepository.SaveChangesAsync(ct);

        Email notification = emailFactory.CreateAssessmentRequestEmail(
            manager.Email!,
            $"{employee.FirstNameEn} {employee.LastNameEn}");

        await emailRepository.AddAsync(notification, ct);
        await emailRepository.SaveChangesAsync(ct);
    }
}
