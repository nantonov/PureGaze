using PureGaze.Application.Contracts.Application;

namespace PureGaze.Application.UseCases.Admin.Employees.GetEmployees;

public sealed record GetEmployeesResult(int Total, IReadOnlyList<EmployeeDto> Employees);