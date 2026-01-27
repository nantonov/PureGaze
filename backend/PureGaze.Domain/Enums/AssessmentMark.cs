using System.Text.Json.Serialization;
using PureGaze.Domain.Converters;

namespace PureGaze.Domain.Enums;

[JsonConverter(typeof(EnumTypeConverter<AssessmentMark>))]
public enum AssessmentMark
{
    NeedsImprovement = 1,
    Competent = 2,
    Excellent = 3
}