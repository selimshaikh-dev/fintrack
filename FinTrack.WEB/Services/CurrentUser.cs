using FinTrack.Application.Common.Interfaces;
using System.Security.Claims;

namespace FinTrack.WEB.Services
{
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        public string Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);
        public string Role => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    }
}
