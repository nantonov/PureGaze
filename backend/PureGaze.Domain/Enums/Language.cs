using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PureGaze.Domain.Converters;

namespace PureGaze.Domain.Enums;

[JsonConverter(typeof(EnumTypeConverter<Language>))]
public enum Language
{
    [EnumMember(Value = "en")]
    En = 1,
    [EnumMember(Value = "ru")]
    Ru = 2
}