using Management.Application.Abstractions.Services;
using Management.Application.Contracts.Integrations.Hrm;
using Management.Infrastructure.Integrations.Hrm.Responses;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Management.Infrastructure.Integrations.Hrm;

public class HrmService(
    IHttpClientFactory httpClientFactory, 
    IOptions<HrmOptions> options) 
    : IHrmService
{
    private readonly HrmOptions _options = options.Value;

    public async Task<EmployeesInfo?> GetEmployeesAsync()
    {
        var client = httpClientFactory.CreateClient(HrmOptions.EmployeeClientName);

        try
        {
            var tokenResponse = await GetAccessTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse?.Token);

            var response =
                await client.GetAsync($"{_options.EmployeeApiUrl}/api/employee-management/api/v2/employees/filter/values");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GetEmployeesResponse>(content);

            return new EmployeesInfo
            {
                Employees = result?.EmployeeManagerIdList != null 
                    ? [.. result.EmployeeManagerIdList.Select(x =>
                        new EemployeeDto
                        {
                            Id = x.Id,
                            FirstNameRu = x.FirstNameRu,
                            LastNameRu = x.LastNameRu,
                            FirstNameEn = x.FirstNameEn,
                            LastNameEn = x.LastNameEn
                        })] 
                    : []
            };
        }
        catch //(Exception e)
        {
            return null;
        }
    }

    public async Task<GetAccessTokenResponse?> GetAccessTokenAsync()
    {
        var client = httpClientFactory.CreateClient(HrmOptions.KeycloakClientName);

        try
        {
            var form = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type",  "password" },
                    { "client_id",  _options?.ClientId ?? ""},
                    { "username",  _options?.Username ?? "" },
                    { "password",  _options?.Password ?? "" }
                });

            var response =
                await client.PostAsync($"{_options?.KeycloakUrl}/auth/realms/innowise-group/protocol/openid-connect/token", form);

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetAccessTokenResponse>(content);
        }
        catch //(Exception e)
        {
            return null;
        }
    }
}