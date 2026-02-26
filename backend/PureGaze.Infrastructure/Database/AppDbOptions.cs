namespace PureGaze.Infrastructure.Database;

public class AppDbOptions
{
    public const string SectionName = "DatabaseOptions";

    public string ConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; }
    public int MaxRetryDelaySecond { get; set; }
}