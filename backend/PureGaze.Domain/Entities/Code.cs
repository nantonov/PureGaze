namespace PureGaze.Domain.Entities;

public class Code : BaseEntity<int>
{
    public Guid GradeId { get; set; }
    public Guid ToGradeId { get; set; }
    public string? Display { get; set; }
    public int TotalEx { get; set; }
    public int DiffEx { get; set; }
    public ICollection<CodeTranslate> CodeTranslates { get; set; } = [];
}