namespace PureGaze.Domain.Entities;

public class Employee : BaseEntity<int>
{
    public string? FirstNameEn { get; set; }
    public string? LastNameEn { get; set; }
    public string? Email { get; set; }
    
    public Guid? ProfessionalLevelId { get; set; }
    public ProfessionalLevel? ProfessionalLevel { get; set; }
    
    public Guid? ManagerialLevelId { get; set; }
    public ManagerialLevel? ManagerialLevel { get; set; }
    
    public int? ManagerId { get; set; }
    public Employee? Manager  { get; set; } 
    
    public int? HeadId { get; set; }
    public Employee? Head  { get; set; }
    
    public int? RMId { get; set; }
    public Employee? RM  { get; set; }
    
    public int? M1Id { get; set; }
    public Employee? M1 { get; set; }
    
    public int? M2Id { get; set; }
    public Employee? M2 { get; set; }
    
    public int? M3Id { get; set; }
    public Employee? M3 { get; set; }
    
    public int? M4Id { get; set; }
    public Employee? M4 { get; set; }

    public EmployeeSettings? EmployeeSettings { get; set; }
    
    public DateTime HireDate { get; set; }
    public DateTime? TerminationDate { get; set; }
    public string? LifecycleStatus { get; set; }
    public ulong Hash { get; set; }
}