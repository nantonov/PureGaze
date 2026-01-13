namespace PureGaze.Application.Contracts.Integrations.Hrm;

public class DictionariesDto
{
    public IList<BaseDictionaryDto> ManagerialLevels { get; set; } = [];
    
    public IList<BaseDictionaryDto> ProfessionalLevels { get; set; } = [];
    
    public IList<BaseDictionaryDto> ProcessConfirmationStatuses { get; set; } = [];
}

public class BaseDictionaryDto
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string Translation { get; set; } = string.Empty;
    public Guid ValueId { get; set; }
    public string Value { get; set; } = string.Empty;
    public int? OrderValue { get; set; }
}