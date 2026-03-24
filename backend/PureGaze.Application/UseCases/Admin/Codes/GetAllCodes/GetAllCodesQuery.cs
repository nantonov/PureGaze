using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.GetAllCodes;

public record GetAllCodesQuery : IRequest<List<CodeDto>>;