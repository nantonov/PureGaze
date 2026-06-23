using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.GetCode;

public record GetCodesQuery(int Id) : IRequest<GetCodeResult>;
