using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Notification.GetEmails;

public class GetEmailsHandler(IEmailRepository emailRepository)
    : IRequestHandler<GetEmailsQuery, GetEmailsResponse>
{
    public async Task<GetEmailsResponse> Handle(GetEmailsQuery query, CancellationToken ct)
    {
        var emails = await emailRepository.GetEmailsAsync(query.Page, query.PageSize, query.Status, ct);

        return new GetEmailsResponse([.. emails.Select(EmailDto.ToDto)]);
    }
}