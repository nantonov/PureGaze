namespace Assessment.Domain.Entities;

public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set; } = null!;
    public Guid SubTopicId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}