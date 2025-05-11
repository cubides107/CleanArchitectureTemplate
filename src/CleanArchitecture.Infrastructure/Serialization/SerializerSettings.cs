
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.Serialization;

public static class SerializerSettings
{
    public static readonly JsonSerializerOptions Instance = new()
    {
        AllowOutOfOrderMetadataProperties = true
    };
}
