using System.Text.Json.Serialization;

namespace SafeSpace.Domain.Entities
{
    public class ZenQuoteResponse
    {
        [JsonPropertyName("q")]
        public string Text { get; set; }

        [JsonPropertyName("a")]
        public string Author { get; set; }
    }
}
