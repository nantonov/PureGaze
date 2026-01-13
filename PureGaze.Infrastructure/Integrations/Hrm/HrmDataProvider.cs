using PureGaze.Application.Contracts.Integrations.Hrm;
using PureGaze.Infrastructure.Integrations.Hrm.Responses;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json; 
using System.Web;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Infrastructure.Integrations.Hrm.Requests;

namespace PureGaze.Infrastructure.Integrations.Hrm;

public class HrmDataProvider(
    IHttpClientFactory httpClientFactory, 
    IOptions<HrmOptions> options) 
    : IHrmDataProvider
{
    private readonly HrmOptions _options = options.Value;

    public async IAsyncEnumerable<IReadOnlyList<EmployeeDto>> GetEmployeesAsync([EnumeratorCancellation] CancellationToken ct)
    {
        var client = httpClientFactory.CreateClient(HrmOptions.EmployeeClientName);
        
        var tokenResponse = await GetAccessTokenAsync(ct);
        
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", tokenResponse?.Token);

        var request = new GetEmployeesRequest
        {
            JobTitleId = new JobTitleId
            {
                In = new List<string>
                {
                    Constants.JobTitles.DotNetDeveloperId,
                    Constants.JobTitles.DotNetMAUIDeveloperId,
                    Constants.JobTitles.HeadOfDotNetId
                }
            }
        };
            
        var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        int pageNumber = 0;

        while (true)
        {
            var response = 
                await client.PostAsync(
                    $"{_options.EmployeeApiUrl}/api/employee-management/api/v2/employees/search?page={pageNumber}&size={_options.PageSize}", stringContent, ct);

            var content = await response.Content.ReadAsStringAsync(ct);

            var result = JsonSerializer.Deserialize<GetEmployeesResponse>(content);
            
            if(result?.Eemployees != null) 
                yield return [.. result.Eemployees.Select(HrmEemployee.ToDto)];
            
            if (pageNumber >= result?.TotalPages)
                yield break;
            
            pageNumber++;   
        }
    }
    
    public async Task<DictionariesDto?> GetDictionariesAsync(CancellationToken ct)
    {
        var client = httpClientFactory.CreateClient(HrmOptions.EmployeeClientName);

        var tokenResponse = await GetAccessTokenAsync(ct);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse?.Token);
        
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["defaultLanguageOnly"] = "true";
        query["filter"] = JsonSerializer.Serialize(new
        {
            name = new []
            {
                Constants.DictionaryTitles.ManagerialLevel,
                Constants.DictionaryTitles.ProfessionalLevel,
                Constants.DictionaryTitles.ProcessConfirmationStatuses,
            }
        });
            
        var response =
            await client.GetAsync($"{_options.EmployeeApiUrl}/api/dictionaries/api/v2/dictionary-translations/filter?{query}", ct);

        var content = await response.Content.ReadAsStringAsync(ct);

        var result = JsonSerializer.Deserialize<GetDictionariesResponse>(content);

        return new DictionariesDto
        { 
            ManagerialLevels = [.. result?.ManagerialLevels.Select(BaseDictionary.ToDto)!],
            ProfessionalLevels = [.. result?.ProfessionalLevels.Select(BaseDictionary.ToDto)!],
            ProcessConfirmationStatuses = [.. result?.ProcessConfirmationStatuses.Select(BaseDictionary.ToDto)!]
        };
    }

    private async Task<GetAccessTokenResponse?> GetAccessTokenAsync(CancellationToken ct)
    {
        var client = httpClientFactory.CreateClient(HrmOptions.KeycloakClientName);
        
        var form = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                { "grant_type",  "password" },
                { "client_id",  _options?.ClientId ?? ""},
                { "username",  _options?.Username ?? "" },
                { "password",  _options?.Password ?? "" }
            });

        var response =
            await client.PostAsync(
                $"{_options?.KeycloakUrl}/auth/realms/innowise-group/protocol/openid-connect/token", form, ct);

        var content = await response.Content.ReadAsStringAsync(ct);

        return JsonSerializer.Deserialize<GetAccessTokenResponse>(content);
    }
}