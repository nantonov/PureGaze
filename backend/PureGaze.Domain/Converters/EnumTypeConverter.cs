using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PureGaze.Domain.Converters;

public class EnumTypeConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Type type = typeof(T);
        int max = Enum.GetValues(typeof(T)).Cast<int>().Max();
        int min = Enum.GetValues(typeof(T)).Cast<int>().Min();
        (int? intResult, string? strResult) = GetResults(ref reader);

        if (!string.IsNullOrEmpty(strResult))
        {
            foreach (FieldInfo field in type.GetFields())
            {
                EnumMemberAttribute? attribute = field.GetCustomAttribute<EnumMemberAttribute>();
                if (attribute != null && attribute.Value == strResult)
                {
                    return (T)field.GetValue(null)!;
                }
            }

            if (Enum.TryParse(strResult, ignoreCase: false, out T result))
            {
                if (Enum.IsDefined(result))
                {
                    return result;
                }
            }
        }
        else if (intResult >= min && intResult <= max)
        {
            if (Enum.IsDefined(type, intResult))
            {
                return (T)Enum.ToObject(typeof(T), intResult);
            }
        }

        throw new JsonException($"Unknown value for enum '{type.Name}'");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        Type type = typeof(T);
        FieldInfo? field = type.GetField(value.ToString());

        if (field == null)
            throw new JsonException($"Unknown enum type '{type.Name}'");

        EnumMemberAttribute? attribute = (EnumMemberAttribute?)field
            .GetCustomAttributes(typeof(EnumMemberAttribute), false)
            .FirstOrDefault();

        writer.WriteStringValue(attribute != null ? attribute.Value : value.ToString());
    }

    private static (int? intResult, string? strResult) GetResults(ref Utf8JsonReader reader)
    {
        string? strResult = null;
        int? intResult = null;

        if (reader.TokenType == JsonTokenType.String)
            strResult = reader.GetString();
        else if (reader.TryGetInt32(out int value))
            intResult = value;

        return (intResult, strResult);
    }
}