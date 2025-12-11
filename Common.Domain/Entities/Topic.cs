namespace Common.Domain.Entities;

public class Topic : BaseDictionaryEntity
{
    public string Name { get; set; } = null!;
    public Guid TemplateId { get; set; }
}