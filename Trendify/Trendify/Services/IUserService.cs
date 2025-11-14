using Microsoft.AspNetCore.Identity;

namespace Trendify.Services
{
    public interface IUserService
    {
        Task<IdentityUser> GetCurrentUserAsync(string userId);
        Task<string> GetUserEmailAsync(string userId);
    }
}