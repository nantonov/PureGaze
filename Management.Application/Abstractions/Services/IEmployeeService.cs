using Management.Application.Contracts.Application;

namespace Management.Application.Abstractions.Services;

public interface IEmployeeService
{
    Task<UploadEmployeesDto> UploadEmployeesAsync(CancellationToken ct);
}