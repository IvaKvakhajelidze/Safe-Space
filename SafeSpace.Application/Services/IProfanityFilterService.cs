namespace SafeSpace.Application.Services
{
    public interface IProfanityFilterService
    {
        Task<string> CleanTextAsync(string text);
        Task<bool> ContainsProfanityAsync(string text);
    }
}
