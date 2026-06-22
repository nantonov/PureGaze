using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.ProfessionalLevels.GetProfessionalLevels;

public class GetProfessionalLevelsHandler(IDictionaryRepository<ProfessionalLevel> repository)
    : IRequestHandler<GetProfessionalLevelsQuery, IReadOnlyList<GetProfessionalLevelsResult>>
{
    public async Task<IReadOnlyList<GetProfessionalLevelsResult>> Handle(GetProfessionalLevelsQuery query, CancellationToken ct = default)
    {
        IReadOnlyList<ProfessionalLevel> levels = await repository.GetAllAsync(ct);
        return
            [.. levels.OrderBy(l => l.OrderValue).Select(l => new GetProfessionalLevelsResult(l.Id, l.Value, l.OrderValue))];
    }
}
