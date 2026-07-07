using SafeSpace.Application.Services;

namespace SafeSpace.Infrastructure.Services
{
    public class ProfanityFilterService : IProfanityFilterService
    {
        private readonly HttpClient _client;

        public ProfanityFilterService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> CleanTextAsync(string text)
        {
            return await SendRequestAsync($"https://www.purgomalum.com/service/plain?text={text}");
        }

        public async Task<bool> ContainsProfanityAsync(string text)
        {
            var result = await SendRequestAsync($"https://www.purgomalum.com/service/containsprofanity?text={text}");

            return bool.Parse(result);
        }

        private async Task<string> SendRequestAsync(string url)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            request.Headers.Add("x-rapidapi-key", "YOUR_KEY");

            request.Headers.Add("x-rapidapi-host", "community-purgomalum.p.rapidapi.com");

            using var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
