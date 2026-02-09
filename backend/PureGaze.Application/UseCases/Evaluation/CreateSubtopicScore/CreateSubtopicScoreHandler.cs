using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Evaluation.CreateSubtopicScore;

public class CreateSubtopicScoreHandler(
    ISubtopicScoreRepository subtopicScoreRepository,
    IAssessmentStageRepository assessmentStageRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<CreateSubtopicScoreCommand>
{
    public async Task Handle(CreateSubtopicScoreCommand request, CancellationToken ct)
    {
        var assessmentStage = await assessmentStageRepository.GetByIdAsync(request.StageId, ct)
            ?? throw new KeyNotFoundException($"Assessment Stage with Id {request.StageId} was not found.");

        if(assessmentStage.Assessor?.Email != currentUserContextProvider.GetUserEmail())
            throw new ValidationException($"Only assessor can set subtopic score");

        var subtopicScore = new SubtopicScore
        {
            SubtopicId = request.SubtopicId,
            StageId = request.StageId,
            Score = request.Score,
            Comment = request.Comment
        };

        await subtopicScoreRepository.AddAsync(subtopicScore, ct);
        await subtopicScoreRepository.SaveChangesAsync(ct);
    }
}
