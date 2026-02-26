using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public sealed record GetCurrentEmployeeQuery : IRequest<GetCurrentEmployeeResponse>;