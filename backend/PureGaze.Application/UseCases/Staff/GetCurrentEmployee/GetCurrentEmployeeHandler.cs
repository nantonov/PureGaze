using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public class GetCurrentEmployeeHandler(IEmployeeRepository repository, ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<GetCurrentEmployeeQuery, GetCurrentEmployeeResult>
{
    public async Task<GetCurrentEmployeeResult> Handle(GetCurrentEmployeeQuery request, CancellationToken ct)
    {
        string email = currentUserContextProvider.GetUserEmail();
        Employee employee = await repository.GetByEmailAsync(email, ct)
            ?? throw new KeyNotFoundException($"Employee with email {email} not found");

        return GetCurrentEmployeeResult.ToResult(employee);
    }
}
