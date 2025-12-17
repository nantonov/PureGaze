namespace Common.Domain.Entities;

public class Topic : BaseEntity<int>
{
    public int TemplateId { get; set; }
    public virtual ICollection<TopicTranslate> TopicTranslates { get; set; } = [];
}