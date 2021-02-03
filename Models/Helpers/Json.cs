using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tach.Models.Helpers {
    public class JSON {

        public static string Parse<T>(T t) {
            var serializeOptions = new JsonSerializerOptions {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(t, serializeOptions);
        }
        
    }
}