using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeLanguage;

public sealed record UpdateEmployeeLanguageCommand(Language Language) : IRequest;