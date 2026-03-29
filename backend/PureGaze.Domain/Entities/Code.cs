namespace PureGaze.Domain.Entities;

public class Code : BaseEntity<int>
{
    public Guid? GradeId { get; set; }
    public ProfessionalLevel? Grade { get; set; }
    public Guid? ToGradeId { get; set; }
    public ProfessionalLevel? ToGrade { get; set; }
    public string? Name { get; set; }
    public int TotalEx { get; set; }
    public int DiffEx { get; set; }
}