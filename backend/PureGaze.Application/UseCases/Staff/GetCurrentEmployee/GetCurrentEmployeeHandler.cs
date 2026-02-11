using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public class GetCurrentEmployeeHandler(IEmployeeRepository repository, ICurrentUserContextProvider currentUserContextProvider) 
    : IRequestHandler<GetCurrentEmployeeQuery, GetCurrentEmployeeResponse>
{
    public async Task<GetCurrentEmployeeResponse> Handle(GetCurrentEmployeeQuery request, CancellationToken ct)
    {
        var email = currentUserContextProvider.GetUserEmail();
        var employee = await repository.GetByEmailAsync(email, ct)
            ??  throw new NullReferenceException($"Employee with email {email} not found");
        
        return GetCurrentEmployeeResponse.ToResult(employee);
    }
}