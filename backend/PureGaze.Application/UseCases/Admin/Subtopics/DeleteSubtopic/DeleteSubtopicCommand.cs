using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Subtopics.DeleteSubtopic;

public sealed record DeleteSubtopicCommand(int Id) : IRequest;
