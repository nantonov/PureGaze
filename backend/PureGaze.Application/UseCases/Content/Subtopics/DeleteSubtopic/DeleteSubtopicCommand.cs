using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Subtopics.DeleteSubtopic;

public record DeleteSubtopicCommand(int Id) : IRequest;
