namespace Assessment.Domain.Entities;
public class Template
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CodeId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}