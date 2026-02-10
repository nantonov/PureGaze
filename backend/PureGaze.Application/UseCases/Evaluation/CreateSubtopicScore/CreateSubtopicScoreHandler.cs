using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Evaluation.CreateSubtopicScore;

public class CreateSubtopicScoreHandler(
    ISubtopicScoreRepository subtopicScoreRepository,
    ISubtopicRepository subtopicRepository,
    IAssessmentStageRepository assessmentStageRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<CreateSubtopicScoreCommand>
{
    public async Task Handle(CreateSubtopicScoreCommand request, CancellationToken ct)
    {
        var assessmentStage = await assessmentStageRepository.GetByIdAsync(request.StageId, ct)
            ?? throw new KeyNotFoundException($"Assessment Stage with Id {request.StageId} was not found.");

        var subtopic = await subtopicRepository.GetByIdAsync(request.SubtopicId, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {request.SubtopicId} was not found.");

        if (assessmentStage.Assessor?.Email != currentUserContextProvider.GetUserEmail())
            throw new ValidationException($"Only assessor can set subtopic score");

        var subtopicScore = await subtopicScoreRepository.GetBySubtopicAndStageIdAsync(request.SubtopicId, request.StageId, ct);

        if(subtopicScore == null)
        {
            subtopicScore = new SubtopicScore
            {
                SubtopicId = request.SubtopicId,
                StageId = request.StageId,
                Score = request.Score,
                Comment = request.Comment
            };
            await subtopicScoreRepository.AddAsync(subtopicScore, ct);
        }
        else
        {
            subtopicScore.Score = request.Score;
            subtopicScore.Comment = request.Comment;
            await subtopicScoreRepository.UpdateAsync(subtopicScore, ct);
        }

        await subtopicScoreRepository.SaveChangesAsync(ct);
    }
}
