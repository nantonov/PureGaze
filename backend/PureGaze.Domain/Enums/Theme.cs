using System.Text.Json.Serialization;
using PureGaze.Domain.Converters;

namespace PureGaze.Domain.Enums;

[JsonConverter(typeof(EnumTypeConverter<Theme>))]
public enum Theme
{
    Dark = 1,
    Light = 2
}