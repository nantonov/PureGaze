namespace Assessment.Common.Options;

public class PostgreOptions
{
    public const string SectionName = "PostgreOptions";
    
    public string ConnectionString { get; set; } = string.Empty;
}
