using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.ProfessionalLevels.GetProfessionalLevels;

public record GetProfessionalLevelsQuery : IRequest<List<ProfessionalLevelDto>>;

public record ProfessionalLevelDto(Guid Id, string? Value);
