using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Codes.GetCode;

public class GetCodesHandler(ICodeRepository codeRepository) : IRequestHandler<GetCodesQuery, GetCodeResult>
{
    public async Task<GetCodeResult> Handle(GetCodesQuery query, CancellationToken ct)
    {
        Code code = await codeRepository.GetByIdAsync(query.Id, ct)
                   ?? throw new KeyNotFoundException($"Code with id: {query.Id} not found");

        return GetCodeResult.ToResult(code);
    }
}
