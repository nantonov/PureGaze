namespace Common.Domain.Entities;

public class Employee : BaseEntity<int>
{
    public string? FirstNameEn { get; set; }
    public string? LastNameEn { get; set; }
    public Guid? ProfessionalLevelValueId { get; set; }
    public Guid? ManagerialLevelValueId { get; set; }
    public string? Email { get; set; }
    public int? ManagerId { get; set; }
    public int? HeadId { get; set; }
    public int? RMId { get; set; }
    public int? M1Id { get; set; }
    public int? M2Id { get; set; }
    public int? M3Id { get; set; }
    public int? M4Id { get; set; }   
}