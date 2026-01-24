using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public class GetCurrentEmployeeHandler(IEmployeeRepository repository) 
    : IRequestHandler<GetCurrentEmployeeQuery, GetCurrentEmployeeResponse>
{
    public async Task<GetCurrentEmployeeResponse> Handle(GetCurrentEmployeeQuery request, CancellationToken ct)
    {
        var employee = await repository.GetByEmailAsync(request.Email, ct)
            ??  throw new NullReferenceException($"Employee with email {request.Email} not found");
        
        return GetCurrentEmployeeResponse.ToResult(employee);
    }
}