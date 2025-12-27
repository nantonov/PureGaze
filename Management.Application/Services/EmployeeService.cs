using Common.Domain.Entities;
using Management.Application.Abstractions.Database;
using Management.Application.Abstractions.Providers;
using Management.Application.Abstractions.Services;
using Management.Application.Contracts.Application;
using Management.Application.Extensions;

namespace Management.Application.Services;

public class EmployeeService(
    IHrmDataProvider hrmDataProvider,
    IEmployeeRepository employeeRepository)
    : IEmployeeService
{
    public async Task<UploadEmployeesDto> UploadEmployeesAsync(CancellationToken ct)
    {
        var result = new UploadEmployeesDto();
        
        await foreach (var hrmEmployees in hrmDataProvider.GetEmployeesAsync(ct))
        {
            IList<int> hrmIds = [.. hrmEmployees.Select(x => x.Id)];
            
            IDictionary<int, Employee> existingEmployees = 
                await employeeRepository.GetEmployeesByIdsAsync(hrmIds, ct);
            
            foreach (var hrmEmployee in hrmEmployees)
            {
                if (!existingEmployees.TryGetValue(hrmEmployee.Id, out var existing))
                {
                    await employeeRepository.AddAsync(hrmEmployee.ToEntity(), ct);
                    result.Created += 1;
                }
                else if (existing.Hash != hrmEmployee.Hash)
                {
                    existing.Update(hrmEmployee);
                    result.Updated += 1;
                }
            }
            
            result.Total += hrmEmployees.Count;
            await employeeRepository.SaveChangesAsync(ct);
        }

        return result;
    }
}