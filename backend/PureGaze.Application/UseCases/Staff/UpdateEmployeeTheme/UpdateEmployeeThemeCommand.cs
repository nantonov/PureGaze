using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeTheme;

public sealed record UpdateEmployeeThemeCommand(Theme Theme) : IRequest;