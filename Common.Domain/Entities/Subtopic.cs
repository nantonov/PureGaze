namespace Common.Domain.Entities;

public class Subtopic : BaseEntity<int>
{
    public ICollection<SubtopicTranslate> SubtopicTranslates { get; set; } = null!;
    public int TopicId { get; set; }
}