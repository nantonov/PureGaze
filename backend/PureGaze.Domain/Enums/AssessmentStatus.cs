using System.Text.Json.Serialization;
using PureGaze.Domain.Converters;

namespace PureGaze.Domain.Enums;

[JsonConverter(typeof(EnumTypeConverter<AssessmentStatus>))]
public enum AssessmentStatus
{
    Created = 1,
    InProgress = 2,
    Finished = 3,
    Closed = 4
}