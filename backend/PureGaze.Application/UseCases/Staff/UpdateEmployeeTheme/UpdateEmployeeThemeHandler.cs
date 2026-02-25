using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeTheme;

public class UpdateEmployeeThemeHandler(
    IEmployeeRepository employeeRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<UpdateEmployeeThemeCommand>
{
    public async Task Handle(UpdateEmployeeThemeCommand request, CancellationToken ct)
    {
        // TODO: Not currently called from frontend (theme is stored only in localStorage).
        var email = currentUserContextProvider.GetUserEmail();
        var employee = await employeeRepository.GetByEmailAsync(email, ct)
                       ?? throw new KeyNotFoundException($"Employee with Email {email} not found.");

        employee.EmployeeSettings ??= new EmployeeSettings { EmployeeId = employee.Id };
        employee.EmployeeSettings.Theme = request.Theme;

        await employeeRepository.SaveChangesAsync(ct);
    }
}