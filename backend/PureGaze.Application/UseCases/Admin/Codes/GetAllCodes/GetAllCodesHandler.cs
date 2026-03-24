using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using PureGaze.Application.UseCases.Admin.Codes.GetCodes;

namespace PureGaze.Application.UseCases.Admin.Codes.GetAllCodes;

public class GetAllCodesHandler(ICodeRepository codeRepository)
    : IRequestHandler<GetAllCodesQuery, List<CodeDto>>
{
    public async Task<List<CodeDto>> Handle(GetAllCodesQuery query, CancellationToken ct = default)
    {
        var (items, _) = await codeRepository.GetAllAsync(ct);
        return items.Select(c => c.ToDto()).ToList();
    }
}