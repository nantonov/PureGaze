namespace Assessment.Common.Options;

public class MsSqlServerOptions
{
    public const string SectionName = "SqlServerOptions";
    
    public string ConnectionString { get; set; } = string.Empty;
}
