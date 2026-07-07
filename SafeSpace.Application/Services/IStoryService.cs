using SafeSpace.Domain.Entities;

namespace SafeSpace.Application.Services
{
    public interface IStoryService : IService<Story>
    {
        Task<IEnumerable<Story>> GetStoriesWithCommentsAsync();
        Task<IEnumerable<Story>> GetStoriesByUserAsync(Guid userId);
    }
}
