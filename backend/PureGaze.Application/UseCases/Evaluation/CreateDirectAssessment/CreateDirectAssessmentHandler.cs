using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.CreateDirectAssessment;

public class CreateDirectAssessmentHandler(
    IEmployeeRepository employeeRepository,
    ICodeRepository codeRepository,
    ITemplateRepository templateRepository,
    IAssessmentRepository assessmentRepository,
    IEmailFactory emailFactory,
    IEmailRepository emailRepository)
    : IRequestHandler<CreateDirectAssessmentCommand>
{
    public async Task Handle(CreateDirectAssessmentCommand command, CancellationToken ct = default)
    {
        var employee = await employeeRepository.GetByIdAsync(command.EmployeeId, ct)
            ?? throw new KeyNotFoundException($"Employee with Id {command.EmployeeId} not found.");

        ValidateManagerIsLinkedToEmployee(command.ManagerId, employee);

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

        var email = emailFactory.CreateAssessmentCreatedByManagerEmail(
            employee.Email!,
            $"{employee.FirstNameEn} {employee.LastNameEn}");

        await emailRepository.AddAsync(email, ct);
        await emailRepository.SaveChangesAsync(ct);
    }

    private static void ValidateManagerIsLinkedToEmployee(int managerId, Employee employee)
    {
        var isM1 = employee.M1Id == managerId;
        var isM3 = employee.M3Id == managerId;

        if (!isM1 && !isM3)
            throw new UnauthorizedAccessException(
                $"Manager with Id {managerId} is not linked to Employee with Id {employee.Id} as M1 or M3.");
    }
}
