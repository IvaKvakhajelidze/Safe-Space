using SafeSpace.Domain.Entities;

namespace SafeSpace.Application.Services
{
    public interface IQuoteService
    {
        Task<ZenQuoteResponse?> GetRandomQuoteAsync();
    }
}
