namespace Management.Application.Abstractions.Services;

public interface IEmployeeService
{
    Task UploadEmployeesAsync(CancellationToken ct);
}