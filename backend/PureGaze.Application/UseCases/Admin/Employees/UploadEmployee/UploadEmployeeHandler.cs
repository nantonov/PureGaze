using Microsoft.Extensions.DependencyInjection;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Employees.UploadEmployee;

public class UploadEmployeeHandler(
    IServiceScopeFactory scopeFactory,
    IHrmDataProvider hrmDataProvider)
    : IRequestHandler<UploadEmployeeCommand>
{
    public async Task Handle(UploadEmployeeCommand command, CancellationToken ct = default)
    {
        await Parallel.ForEachAsync(hrmDataProvider.GetEmployeesAsync(ct),
            new ParallelOptions { MaxDegreeOfParallelism = 3 },
            async (hrmEmployees, cancellation) =>
            {
                await using AsyncServiceScope scope = scopeFactory.CreateAsyncScope();
                IEmployeeRepository employeeRepo = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();

                IReadOnlyList<int> hrmIds = [.. hrmEmployees.Select(x => x.Id)];

                IDictionary<int, Employee> existingEmployees =
                    await employeeRepo.GetByIdsAsync(hrmIds, cancellation);

                foreach (HrmEmployeeDto? hrmEmployee in hrmEmployees)
                {
                    if (!existingEmployees.TryGetValue(hrmEmployee.Id, out Employee? existing))
                        await employeeRepo.AddAsync(UploadEmployeeCommand.ToEntity(hrmEmployee), cancellation);

                    else if (existing.Hash != hrmEmployee.Hash)
                        UploadEmployeeCommand.Update(existing, hrmEmployee);
                }

                await employeeRepo.SaveChangesAsync(cancellation);
            });
    }
}
