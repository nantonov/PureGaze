using Common.Domain.Entities;
using Management.Application.Abstractions.Database;
using Management.Application.Abstractions.Providers;
using Management.Application.Abstractions.Services;

namespace Management.Application.Services;

public class EmployeeService(
    IHrmDataProvider hrmDataProvider,
    IEmployeeRepository employeeRepository)
    : IEmployeeService
{
    public async Task UploadEmployeesAsync(CancellationToken ct)
    {
        var result = new UploadEmployeesResult();
        
        await foreach (var hrmEmployees in hrmDataProvider.GetEmployeesAsync(ct))
        {
            var hrmIds = hrmEmployees.Select(x => x.Id).ToArray();
            
            var existingEmployees = await employeeRepository.GetEmployeesByIdsAsync(hrmIds, ct);

            foreach (var hrmEmployee in hrmEmployees)
            {
                if (existingEmployees.TryGetValue(hrmEmployee.Id, out var existing))
                {
                    existing.UpdatedAt =  DateTime.UtcNow;
                    
                }
                else
                {
                    employeeRepository.AddAsync( 
                        new Employee
                        {
                            
                        }, ct);
                }
            }

            await employeeRepository.SaveChangesAsync(ct);
        }
    }
}

public class UploadEmployeesResult
{
    public int Created { get; set; }
    
    public int Updated { get; set; }
    
    public int Total { get; set; }
}