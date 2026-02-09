using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Notification.GetEmails;

public class GetEmailsResponse(IReadOnlyList<EmailDto> Emails);