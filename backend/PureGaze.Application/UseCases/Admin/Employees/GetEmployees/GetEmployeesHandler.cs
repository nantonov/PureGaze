using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Employees.GetEmployees;

public class GetEmployeesHandler(IEmployeeRepository repository)
    : IRequestHandler<GetEmployeesQuery, GetEmployeesResult>
{
    public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken ct)
    {
        IReadOnlyList<Employee> employees =
            await repository.GetEmployeesAsync(query.Search, query.Page, query.PageSize, ct);

        int count = await repository.GetCountAsync(ct);

        return new GetEmployeesResult(count, [.. employees.Select(GetEmployeesDto.ToDto)]);
    }
}
