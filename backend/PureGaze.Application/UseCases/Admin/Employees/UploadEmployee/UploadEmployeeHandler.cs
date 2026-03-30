using Microsoft.Extensions.DependencyInjection;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Extensions;
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
                await using var scope = scopeFactory.CreateAsyncScope();
                IEmployeeRepository employeeRepo = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();

                IReadOnlyList<int> hrmIds = [.. hrmEmployees.Select(x => x.Id)];

                IDictionary<int, Employee> existingEmployees =
                    await employeeRepo.GetByIdsAsync(hrmIds, cancellation);

                foreach (var hrmEmployee in hrmEmployees)
                {
                    if (!existingEmployees.TryGetValue(hrmEmployee.Id, out var existing))
                        await employeeRepo.AddAsync(hrmEmployee.ToEntity(), cancellation);

                    else if (existing.Hash != hrmEmployee.Hash)
                        existing.Update(hrmEmployee);
                }

                await employeeRepo.SaveChangesAsync(cancellation);
            });
    }
}