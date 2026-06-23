using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Codes.GetAllCodes;

public class GetAllCodesHandler(ICodeRepository codeRepository)
    : IRequestHandler<GetAllCodesQuery, IReadOnlyList<GetAllCodesResult>>
{
    public async Task<IReadOnlyList<GetAllCodesResult>> Handle(GetAllCodesQuery query, CancellationToken ct = default)
    {
        IReadOnlyList<Code> codes = await codeRepository.GetAllAsync(ct);

        return [.. codes.Select(GetAllCodesResult.ToResult)];
    }
}
