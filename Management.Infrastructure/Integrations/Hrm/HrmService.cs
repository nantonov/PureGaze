using Management.Application.Abstractions.Services;
using Management.Application.Contracts.Integrations.Hrm;
using Management.Infrastructure.Integrations.Hrm.Responses;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json; 
using System.Web;
using Management.Infrastructure.Integrations.Hrm.Requests;

namespace Management.Infrastructure.Integrations.Hrm;

public class HrmService(
    IHttpClientFactory httpClientFactory, 
    IOptions<HrmOptions> options) 
    : IHrmService
{
    private readonly HrmOptions _options = options.Value;

    public async IAsyncEnumerable<EemployeeDto> GetEmployeesAsync()
    {
        var client = httpClientFactory.CreateClient(HrmOptions.EmployeeClientName);
        
        var tokenResponse = await GetAccessTokenAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse?.Token);

        var request = new GetEmployeesRequest
        {
            DismissalStatus = new DismissalStatus
            {
                Matchs = "ACTUAL"
            },
            JobTitleId = new JobTitleId
            {
                In = new List<string>
                {
                    "f2812a29-5397-47ae-9bf4-ac2555dd7244",
                    "b750f28e-e921-4562-8643-e911161c795b",
                    "4fe5915d-58c0-4c4b-bec8-011ec7bee430"
                }
            }
        };
            
        var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        int pageNumber = 0;

        while (true)
        {
            var response =
                await client.PostAsync($"{_options.EmployeeApiUrl}/api/employee-management/api/v2/employees/search?page={pageNumber}&size={_options.PageSize}", stringContent);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GetEmployeesResponse>(content);
            
            foreach (var employee in result?.Eemployees ?? [])
                yield return HrmEemployee.ToDto(employee);
            
            
            if (pageNumber >= result?.TotalPages)
                yield break;
            
            pageNumber++;   
        }
    }

    public async Task<DictionariesDto?> GetDictionariesAsync()
    {
        var client = httpClientFactory.CreateClient(HrmOptions.EmployeeClientName);

        try
        {
            var tokenResponse = await GetAccessTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse?.Token);

            
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["defaultLanguageOnly"] = "true";
            query["filter"] = "{\"name\":[\"skillLevel\",\"processConfirmationStatus\",\"yesNoOtherOptions\",\"meetingRequestStatus\",\"professionalLevel\",\"managerialLevel\"]}";
            
            var response =
                await client.GetAsync($"{_options.EmployeeApiUrl}/api/dictionaries/api/v2/dictionary-translations/filter?{query}");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GetDictionariesResponse>(content);

            return new DictionariesDto
            { 
                ManagerialLevels = [.. result?.ManagerialLevels.Select(BaseDictionary.ToDto)!],
                ProfessionalLevels = [.. result?.ProfessionalLevels.Select(BaseDictionary.ToDto)!],
                MeetingRequestStatuses = [.. result?.MeetingRequestStatuses.Select(BaseDictionary.ToDto)!],
                SkillLevels = [.. result?.SkillLevels.Select(BaseDictionary.ToDto)!],
                YesNoOtherOptions = [.. result?.YesNoOtherOptions.Select(BaseDictionary.ToDto)!],
                ProcessConfirmationStatuses = [.. result?.ProcessConfirmationStatuses.Select(BaseDictionary.ToDto)!]
            };
        }
        catch
        {
            throw;
        }
    }

    private async Task<GetAccessTokenResponse?> GetAccessTokenAsync()
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
        catch
        {
            throw;
        }
    }
}