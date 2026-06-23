using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.GetAllCodes;

public record GetAllCodesQuery : IRequest<IReadOnlyList<GetAllCodesResult>>;
