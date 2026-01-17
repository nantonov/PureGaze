using System.Text.Json.Serialization;
using PureGaze.Application.Contracts.Integrations.Hrm;

namespace PureGaze.Infrastructure.Integrations.Hrm.Responses;

public class GetDictionariesResponse
{
    [JsonPropertyName("managerialLevel")]
    public IList<BaseDictionary> ManagerialLevels { get; set; } = [];
    
    [JsonPropertyName("professionalLevel")]
    public IList<BaseDictionary> ProfessionalLevels { get; set; } = [];

    [JsonPropertyName("processConfirmationStatus")]
    public IList<BaseDictionary> ProcessConfirmationStatuses { get; set; } = [];
}

public class BaseDictionary
{
    [JsonPropertyName("valueId")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("translation")]
    public string Translation { get; set; } = string.Empty;
    
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("orderValue")]
    public int? OrderValue { get; set; }
    
    public static BaseDictionaryDto ToDto(BaseDictionary dictionary) 
        => new()
        {
            Id = dictionary.Id,
            Translation = dictionary.Translation,
            Value = dictionary.Value,
            OrderValue =  dictionary.OrderValue
        };
    
}