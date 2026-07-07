using SafeSpace.Domain.Entities;
using SafeSpace.Application.Services;
using System.Text.Json;

namespace SafeSpace.Infrastructure.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly HttpClient _client;

        public QuoteService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ZenQuoteResponse?> GetRandomQuoteAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://zenquotes.io/api/random")
            };

            using var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<ZenQuoteResponse>>(body);

            if (result != null && result.Count > 0)
            {
                return result[0];
            }

            return null;
        }
    }
}
