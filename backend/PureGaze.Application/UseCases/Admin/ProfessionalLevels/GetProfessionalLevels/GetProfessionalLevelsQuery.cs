using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.ProfessionalLevels.GetProfessionalLevels;

public record GetProfessionalLevelsQuery : IRequest<IReadOnlyList<ProfessionalLevelDto>>;
