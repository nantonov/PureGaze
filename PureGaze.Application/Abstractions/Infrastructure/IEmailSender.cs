using PureGaze.Domain.Entities;

namespace PureGaze.Application.Abstractions.Infrastructure;

public interface IEmailSender
{
    Task SendAsync(Email email, CancellationToken ct = default);
}