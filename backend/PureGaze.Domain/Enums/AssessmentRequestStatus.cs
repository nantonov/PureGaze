using System.Text.Json.Serialization;
using PureGaze.Domain.Converters;

namespace PureGaze.Domain.Enums;

[JsonConverter(typeof(EnumTypeConverter<AssessmentRequestStatus>))]
public enum AssessmentRequestStatus
{
    Created = 1,
    UnderReview = 2,
    Approved = 3,
    Rejected = 4
}