using System.Text.Json.Serialization;

namespace Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Cities
{
    Madrid,
    Barcelona,
    Bilbao,
    Valencia,
    Zaragoza,
    Sevilla,
}
