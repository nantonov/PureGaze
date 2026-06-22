using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Notification.GetEmails;

public class GetEmailsHandler(IEmailRepository emailRepository)
    : IRequestHandler<GetEmailsQuery, GetEmailsResult>
{
    public async Task<GetEmailsResult> Handle(GetEmailsQuery query, CancellationToken ct)
    {
        IReadOnlyList<Email> emails = await emailRepository.GetEmailsAsync(query.Page, query.PageSize, query.Status, ct);

        return new GetEmailsResult([.. emails.Select(EmailDto.ToDto)]);
    }
}