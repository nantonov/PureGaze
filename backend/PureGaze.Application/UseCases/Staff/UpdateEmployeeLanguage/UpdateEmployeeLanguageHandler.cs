using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeLanguage;

public class UpdateEmployeeLanguageHandler(
    IEmployeeRepository employeeRepository, 
    ICurrentUserContextProvider currentUserContextProvider) 
    : IRequestHandler<UpdateEmployeeLanguageCommand>
{
    public async Task Handle(UpdateEmployeeLanguageCommand request, CancellationToken ct)
    {
        var email = currentUserContextProvider.GetUserEmail();
        var employee = await employeeRepository.GetByEmailAsync(email, ct)
            ?? throw new KeyNotFoundException($"Employee with Email {email} not found.");
        
        employee.EmployeeSettings?.Language = request.Language;

        await employeeRepository.SaveChangesAsync(ct);
    }
}