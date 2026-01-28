using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeTheme;

public class UpdateEmployeeThemeHandler(IEmployeeRepository employeeRepository) 
    : IRequestHandler<UpdateEmployeeThemeCommand>
{
    public async Task Handle(UpdateEmployeeThemeCommand request, CancellationToken ct)
    {
        var employee = await employeeRepository.GetByEmailAsync(request.Email, ct)
                       ?? throw new KeyNotFoundException($"Employee with Email {request.Email} not found.");
        
        employee.EmployeeSettings.Theme = request.Theme;

        await employeeRepository.SaveChangesAsync(ct);
    }
}