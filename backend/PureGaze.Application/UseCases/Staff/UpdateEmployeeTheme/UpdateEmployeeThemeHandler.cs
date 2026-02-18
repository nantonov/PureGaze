using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeTheme;

public class UpdateEmployeeThemeHandler(
    IEmployeeRepository employeeRepository, 
    ICurrentUserContextProvider currentUserContextProvider) 
    : IRequestHandler<UpdateEmployeeThemeCommand>
{
    public async Task Handle(UpdateEmployeeThemeCommand request, CancellationToken ct)
    {
        var email = currentUserContextProvider.GetUserEmail();
        var employee = await employeeRepository.GetByEmailAsync(email, ct)
                       ?? throw new KeyNotFoundException($"Employee with Email {email} not found.");
        
        employee.EmployeeSettings?.Theme = request.Theme;

        await employeeRepository.SaveChangesAsync(ct);
    }
}