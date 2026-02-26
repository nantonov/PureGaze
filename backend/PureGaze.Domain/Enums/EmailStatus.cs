using System.Text.Json.Serialization;
using PureGaze.Domain.Converters;

namespace PureGaze.Domain.Enums;

[JsonConverter(typeof(EnumTypeConverter<EmailStatus>))]
public enum EmailStatus
{
    InQueue = 1,
    Sending = 2,
    Sent = 3,
    Failed = 4,
    ExceededRetryCount = 5
}