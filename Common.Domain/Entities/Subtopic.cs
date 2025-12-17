namespace Common.Domain.Entities;

public class Subtopic : BaseEntity<int>
{
    public int TopicId { get; set; }
    
    public virtual ICollection<SubtopicTranslate> SubtopicTranslates { get; set; } = null!;
}