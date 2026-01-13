using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Notification.GetEmails;

public class GetEmailsResponse
{
    public IReadOnlyList<EmailDto> Emails { get; set; } = [];
}