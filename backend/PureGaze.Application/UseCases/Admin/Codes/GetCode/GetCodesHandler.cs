using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.GetCode;

public class GetCodesHandler(ICodeRepository codeRepository) : IRequestHandler<GetCodesQuery, GetCodeResult>
{
    public async Task<GetCodeResult> Handle(GetCodesQuery query, CancellationToken ct)
    {
        var code = await codeRepository.GetByIdAsync(query.Id, ct)
                   ?? throw new KeyNotFoundException($"Code with id: {query.Id} not found");

        return code.ToGetCodeResult();
    }
}