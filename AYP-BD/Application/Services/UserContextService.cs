﻿using Application.Common;
using Application.Extensions;
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
        public string GetUserNickName => User.Claims.Count() is 0 ? null : User.FindFirst(c => c.Type == AybClaims.NickName).Value;

        public long? GetUserId => User.Claims.Count() is 0 ? null : User.FindFirst(c => c.Type == AybClaims.UserId).Value.Parse<long>();

        public long? GetUserSteamId => User.Claims.Count() is 0 ? null : User.FindFirst(c => c.Type == AybClaims.SteamId).Value.Parse<long>();
    }

    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        long? GetUserId { get; }
        string GetUserNickName { get; }
        long? GetUserSteamId { get; }
    }
}
