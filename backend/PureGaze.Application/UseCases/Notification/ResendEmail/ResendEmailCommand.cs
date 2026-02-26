using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Notification.ResendEmail;

public sealed record ResendEmailCommand(Guid Id) : IRequest;