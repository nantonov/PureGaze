using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Employees.GetEmployees;

public sealed record GetEmployeesQuery(int Page, int PageSize)
    : IRequest<GetEmployeesResult>;