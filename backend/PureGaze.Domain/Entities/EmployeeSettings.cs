using PureGaze.Domain.Enums;

namespace PureGaze.Domain.Entities;

public class EmployeeSettings
{
    public int EmployeeId { get; set; }
    public Employee Employee  { get; set; } 
    public Language Language { get; set; }
    public Theme Theme { get; set; }
}