using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Evaluation.CreateDirectAssessment;

public class CreateDirectAssessmentHandler(
    IEmployeeRepository employeeRepository,
    ICodeRepository codeRepository,
    ITemplateRepository templateRepository,
    IAssessmentRepository assessmentRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository,
    IEmailQueuePublisher? queuePublisher = null)
    : IRequestHandler<CreateDirectAssessmentCommand>
{
    public async Task Handle(CreateDirectAssessmentCommand command, CancellationToken ct = default)
    {
        var employee = await employeeRepository.GetByIdAsync(command.EmployeeId, ct)
            ?? throw new KeyNotFoundException($"Employee with Id {command.EmployeeId} not found.");

        if (employee.M1Id != command.ManagerId && employee.M3Id != command.ManagerId)
            throw new UnauthorizedAccessException(
                $"Manager with Id {command.ManagerId} is not linked to Employee with Id {employee.Id} as M1 or M3.");

        if (employee.ProfessionalLevelId == null)
            throw new ValidationException($"Professional Level for Employee with Id {command.EmployeeId} is not set.");

        var code = await codeRepository.GetByProfessionalLevelIdAsync(employee.ProfessionalLevelId.Value, ct)
            ?? throw new KeyNotFoundException($"Code for Employee with Id {command.EmployeeId} not found.");

        var template = await templateRepository.GetByCodeIdAsync(code.Id, ct)
            ?? throw new KeyNotFoundException($"Template for Code {code.Id} not found.");

        var assessment = new Assessment
        {
            EmployeeId = command.EmployeeId,
            CodeId = code.Id,
            TemplateId = template.Id,
            Status = AssessmentStatus.Created,
            Stages = template.Topics.Select(topic => new AssessmentStage
            {
                TopicId = topic.Id,
                Status = StageStatus.Pending
            }).ToList()
        };

        await assessmentRepository.AddAsync(assessment, ct);

        var email = emailFactory.CreateAssessmentDirectByManagerEmail(
            employee.Email!,
            $"{employee.FirstNameEn} {employee.LastNameEn}");

        await emailRepository.AddAsync(email, ct);
        await emailRepository.SaveChangesAsync(ct);

        if (queuePublisher is not null)
            await queuePublisher.PublishAsync(email.Id, ct);
    }
}
