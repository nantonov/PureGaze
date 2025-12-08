using System.Text.Json.Serialization;

namespace Management.Infrastructure.Integrations.Hrm.Requests;

public class GetEmployeesRequest
{
    [JsonPropertyName("dismissalStatus")]
    public DismissalStatus? DismissalStatus { get; set; }

    [JsonPropertyName("jobTitleId")]
    public JobTitleId? JobTitleId { get; set; }
}

public class DismissalStatus
{
    [JsonPropertyName("equals")]
    public string? Matchs { get; set; }
}

public class JobTitleId
{
    [JsonPropertyName("in")]
    public IList<string>? In { get; set; }
}