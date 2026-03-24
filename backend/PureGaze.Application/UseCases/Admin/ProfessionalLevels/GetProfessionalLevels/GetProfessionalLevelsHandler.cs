using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.ProfessionalLevels.GetProfessionalLevels;

public class GetProfessionalLevelsHandler(IDictionaryRepository<ProfessionalLevel> repository)
    : IRequestHandler<GetProfessionalLevelsQuery, List<ProfessionalLevelDto>>
{
    public async Task<List<ProfessionalLevelDto>> Handle(GetProfessionalLevelsQuery query, CancellationToken ct = default)
    {
        var levels = await repository.GetAllAsync(ct);
        return levels
            .OrderBy(l => l.OrderValue)
            .Select(l => new ProfessionalLevelDto(l.Id, l.Value))
            .ToList();
    }
}
