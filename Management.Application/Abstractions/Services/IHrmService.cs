namespace Management.Application.Abstractions.Services;

public interface IHrmService
{
    Task UploadEmployeesAsync(CancellationToken ct);
    
    Task UploadDictionariesAsync(CancellationToken ct);
}