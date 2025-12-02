namespace Assessment.Domain.Entities;

public class Code
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GradeId { get; set; }
    public Guid ToGradeId { get; set; }
    public string Display { get; set; } = null!;
    public string LevelVision { get; set; } = null!;
    public int TotalEx { get; set; }
    public int DiffEx { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}