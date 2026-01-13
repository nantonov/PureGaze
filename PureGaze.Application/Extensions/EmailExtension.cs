using PureGaze.Application.Contracts.Application;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.Extensions;

public static class EmailExtension
{
    public static EmailDto ToDto(this Email email)
        => new EmailDto
        {
            From = email.From,
            To = email.To,
            Subject = email.Subject,
            Status = email.Status
        };
}