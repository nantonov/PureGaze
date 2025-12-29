using System.Text.Json.Serialization;
using Management.Application.Contracts.Integrations.Hrm;

namespace Management.Infrastructure.Integrations.Hrm.Responses;

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
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("languageId")]
    public Guid LanguageId { get; set; }
    
    [JsonPropertyName("translation")]
    public string Translation { get; set; } = string.Empty;
    
    [JsonPropertyName("valueId")]
    public Guid ValueId { get; set; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("orderValue")]
    public int? OrderValue { get; set; }
    
    public static BaseDictionaryDto ToDto(BaseDictionary dictionary) 
        => new()
        {
            Id = dictionary.Id,
            LanguageId = dictionary.LanguageId,
            Translation = dictionary.Translation,
            ValueId = dictionary.ValueId,
            Value = dictionary.Value,
            OrderValue =  dictionary.OrderValue
        };
    
}