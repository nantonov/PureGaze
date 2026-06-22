using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.CompleteAssessment;

public sealed class CompleteAssessmentHandler(IAssessmentRepository assessmentRepository)
    : IRequestHandler<CompleteAssessmentCommand>
{
    public async Task Handle(CompleteAssessmentCommand request, CancellationToken ct)
    {
        Assessment assessment = await assessmentRepository.GetByIdAsync(request.AssessmentId, ct)
            ?? throw new KeyNotFoundException($"Assessment with Id {request.AssessmentId} not found.");

        if (assessment.Status != AssessmentStatus.InProgress)
            throw new ValidationException("Only in-progress assessments can be completed.");

        assessment.Status = AssessmentStatus.Finished;
        await assessmentRepository.SaveChangesAsync(ct);
    }
}
