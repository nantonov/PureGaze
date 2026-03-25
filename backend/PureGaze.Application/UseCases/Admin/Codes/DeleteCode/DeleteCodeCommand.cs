using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.DeleteCode;

public sealed record DeleteCodeCommand(int Id) : IRequest;