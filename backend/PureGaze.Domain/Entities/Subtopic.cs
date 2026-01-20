namespace PureGaze.Domain.Entities;

public class Subtopic : BaseEntity<int>
{
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
    public ICollection<SubtopicTranslate> SubtopicTranslates { get; set; } = [];
    public ICollection<Question> Questions { get; set; } = [];
}