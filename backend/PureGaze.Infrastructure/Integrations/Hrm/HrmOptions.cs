namespace PureGaze.Infrastructure.Integrations.Hrm;

public class HrmOptions
{
    public static string SectionName => "HrmOptions";
    public static string EmployeeClientName => "Employee";
    public static string KeycloakClientName => "Keycloak";

    public string? EmployeeApiUrl { get; set; }
    public string? KeycloakUrl { get; set; }
    public string? ClientId { get; set; }
    public string? Password { get; set; }
    public string? Username { get; set; }
    public int PageSize { get; set; } = 20;
}