using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Employees.GetEmployees;

public sealed record GetEmployeesQuery(string Search, int Page, int PageSize)
    : IRequest<GetEmployeesResult>;