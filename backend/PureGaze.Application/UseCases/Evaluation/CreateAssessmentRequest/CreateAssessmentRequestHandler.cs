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
    ICurrentUserContextProvider currentUserContextProvider,
    IEmailQueuePublisher? queuePublisher = null)
    : IRequestHandler<CreateAssessmentRequestCommand>
{
    public async Task Handle(CreateAssessmentRequestCommand assessmentRequestCommand, CancellationToken ct = default)
    {
        var email = currentUserContextProvider.GetUserEmail();

        var employee = await employeeRepository.GetByEmailAsync(email, ct)
            ?? throw new KeyNotFoundException($"Employee with email {email} not found.");

        var manager =
            employee.M1
            ?? employee.M3
            ?? throw new KeyNotFoundException($"Manager for email {email} not found.");

        if (employee.ProfessionalLevelId == null)
            throw new ValidationException($"Current Professional Level for Employee with email {email} is not set.");

        var code = await codeRepository.GetByProfessionalLevelIdAsync(employee.ProfessionalLevelId.Value, ct)
            ?? throw new KeyNotFoundException($"Code for Employee with email {email} not found.");

        var assessmentRequest = await assessmentRequestRepository.GetEmployeeActiveAssessmentRequest(employee.Id, ct);
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

        var notification = emailFactory.CreateAssessmentRequestEmail(
            manager.Email!,
            $"{employee.FirstNameEn} {employee.LastNameEn}");

        await emailRepository.AddAsync(notification, ct);
        await emailRepository.SaveChangesAsync(ct);

        if (queuePublisher is not null)
            await queuePublisher.PublishAsync(notification.Id, ct);
    }
}