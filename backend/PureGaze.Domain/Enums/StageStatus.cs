using System.Text.Json.Serialization;
using PureGaze.Domain.Converters;

namespace PureGaze.Domain.Enums;

[JsonConverter(typeof(EnumTypeConverter<StageStatus>))]
public enum StageStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3
}