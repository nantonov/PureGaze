using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Notification.GetEmails;

public sealed record GetEmailsResponse(IReadOnlyList<EmailDto> Emails);

public sealed record EmailDto(string? From,  string? To, string? Subject, EmailStatus Status)
{
    public static EmailDto ToDto(Email email)
        => new(email.From, email.To, email.Subject, email.Status);
}