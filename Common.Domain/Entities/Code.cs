namespace Common.Domain.Entities;

public class Code : BaseEntity<int>
{
    public Guid GradeId { get; set; }
    public Guid ToGradeId { get; set; }
    public ICollection<CodeTranslate> CodeTranslates { get; set; } = null!;
    public string LevelVision { get; set; } = null!;
    public int TotalEx { get; set; }
    public int DiffEx { get; set; }
}