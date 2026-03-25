using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Employees.GetEmployees;

public class GetEmployeesHandler(IEmployeeRepository repository)
    : IRequestHandler<GetEmployeesQuery, GetEmployeesResult>
{
    public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken ct)
    {
        var employees =
            await repository.GetEmployeesAsync(query.Page, query.PageSize, ct);

        var count = await repository.GetCountAsync(ct);

        return new GetEmployeesResult(count, [..employees.Select(x => x.ToDto())]);
    }
}