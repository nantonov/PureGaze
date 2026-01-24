using System.Text.Json.Serialization;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public class GetCurrentEmployeeResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("managerLevel")]
    public string? ManagerLevel { get; set; }

    public static GetCurrentEmployeeResponse ToResult(Employee empl)
        => new()
        {
            Id = empl.Id,
            FirstName = empl.FirstNameEn,
            LastName = empl.LastNameEn,
            Email =  empl.Email,
            ManagerLevel = empl.ManagerialLevel?.Value
        };
}