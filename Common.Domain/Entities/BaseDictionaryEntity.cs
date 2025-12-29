namespace Common.Domain.Entities;

public class BaseDictionaryEntity : BaseEntity<Guid>
{
    public Guid LanguageId { get; set; }
    public string Translation { get; set; } = string.Empty;
    public Guid ValueId { get; set; }
    public string Value { get; set; } = string.Empty;
    public int? OrderValue { get; set; }
}