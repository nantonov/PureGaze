namespace Common.Domain.Entities;

public class Topic : BaseEntity<int>
{
    public ICollection<TopicTranslate> TopicTranslates { get; set; } = null!;
    public Guid TemplateId { get; set; }
}