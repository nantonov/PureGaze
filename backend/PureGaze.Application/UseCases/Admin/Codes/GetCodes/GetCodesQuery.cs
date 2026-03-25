using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.GetCodes;

public record GetCodesQuery(int Id) : IRequest<CodeDto>;