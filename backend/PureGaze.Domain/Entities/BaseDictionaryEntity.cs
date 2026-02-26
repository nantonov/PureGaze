namespace PureGaze.Domain.Entities;

public class BaseDictionaryEntity : BaseEntity<Guid>
{
    public string? Translation { get; set; }
    public string? Value { get; set; }
    public int? OrderValue { get; set; }
}