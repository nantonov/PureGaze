namespace PureGaze.Domain.Entities;

public class BaseDictionaryEntity : BaseEntity<Guid>
{
    public string Translation { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int? OrderValue { get; set; }
}