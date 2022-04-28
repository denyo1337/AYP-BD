using Application.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public long? GetUserId => User is null ? null : long.Parse(User.FindFirst(c => c.Type == AybClaims.UserId).Value);
    }

    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        long? GetUserId { get; }
    }
}
