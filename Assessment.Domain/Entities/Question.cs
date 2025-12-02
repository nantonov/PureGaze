namespace Assessment.Domain.Entities;

public class Question : BaseEntity
{
    public string Content { get; set; } = null!;
    public Guid SubTopicId { get; set; }
}