using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.UpdateEmployeeLanguage;

public class UpdateEmployeeLanguageHandler(IEmployeeRepository employeeRepository) 
    : IRequestHandler<UpdateEmployeeLanguageCommand>
{
    public async Task Handle(UpdateEmployeeLanguageCommand request, CancellationToken ct)
    {
        var employee = await employeeRepository.GetByEmailAsync(request.Email, ct)
            ?? throw new KeyNotFoundException($"Employee with Email {request.Email} not found.");
        
        employee.EmployeeSettings.Language = request.Language;

        await employeeRepository.SaveChangesAsync(ct);
    }
}