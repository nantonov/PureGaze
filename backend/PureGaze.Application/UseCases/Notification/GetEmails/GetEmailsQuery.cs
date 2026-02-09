using System.Text.Json.Serialization;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Notification.GetEmails;

public sealed record GetEmailsQuery(int Page, int PageSize, EmailStatus Status) : IRequest<GetEmailsResponse>;