namespace PureGaze.Domain.Entities;

public class Topic : BaseEntity<int>
{
    public int TemplateId { get; set; }
    public ICollection<TopicTranslate> TopicTranslates { get; set; } = [];
}