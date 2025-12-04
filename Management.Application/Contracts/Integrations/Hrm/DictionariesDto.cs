namespace Management.Application.Contracts.Integrations.Hrm;

public class DictionariesDto
{
    public IList<BaseDictionaryDto> SkillLevels { get; set; } = [];
    
    public IList<BaseDictionaryDto>? ProcessConfirmationStatuses { get; set; } = [];
    
    public IList<BaseDictionaryDto> YesNoOtherOptions { get; set; } = [];
    
    public IList<BaseDictionaryDto> MeetingRequestStatuses { get; set; } = [];
    
    public IList<BaseDictionaryDto> ProfessionalLevels { get; set; } = [];
    
    public IList<BaseDictionaryDto> ManagerialLevels { get; set; } = [];
}

public class BaseDictionaryDto
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string Translation { get; set; } = string.Empty;
    public Guid ValueId { get; set; }
    public string Value { get; set; } = string.Empty;
}