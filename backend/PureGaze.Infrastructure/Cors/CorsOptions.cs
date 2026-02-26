namespace PureGaze.Infrastructure.Cors;

public class CorsOptions
{
    public const string SectionName = "CorsOptions";
    public const string PolicyName = "AllowUICors";
    public string[] Origins { get; init; } = [];
}