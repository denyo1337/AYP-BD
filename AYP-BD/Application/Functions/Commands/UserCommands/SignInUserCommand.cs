﻿using Application.Common;
using Application.DTO;
using Application.Services;
using Domain.Data.Interfaces;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Functions.Commands.UserCommands
{
    public class SignInUserCommand : IRequest<SignInResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInResultDto>
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUsersRepostiory _usersRepostiory;
        private readonly IUsersServiceValidator _userServiceDataValidator;
        public SignInUserCommandHandler(IUsersRepostiory usersRepostiory, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IUsersServiceValidator userServiceDataValidator)
        {
            _usersRepostiory = usersRepostiory;
            _authenticationSettings = authenticationSettings;
            _userServiceDataValidator = userServiceDataValidator;
        }
        public async Task<SignInResultDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepostiory.GetUser(request.Email, cancellationToken);
            var verificationResult = _userServiceDataValidator.ValidateUserDataAndPassword(user, request.Password);

            if (verificationResult != LoginVerificationResult.Succes) return new(verificationResult);
          
            List<Claim> claims = PrepareClaimsForUserDependedOnCurrentSteamIdValue(user);
            JwtSecurityToken token = CreateJwtTokenWithClaims(claims);

            user.UpdateLastLogOn();
            await _usersRepostiory.SaveChangesAsync(cancellationToken);

            return new(WriteToken(token), LoginVerificationResult.Succes);
        }

        private static List<Claim> PrepareClaimsForUserDependedOnCurrentSteamIdValue(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(AybClaims.UserId, user.Id.ToString()),
                new Claim(AybClaims.EmailAddress, user.Email),
                new Claim(AybClaims.NickName, string.IsNullOrEmpty(user.NickName) ? user.Email : user.NickName),
                new Claim(AybClaims.Role, user.Role.Name),
            };
            if (user.SteamId.HasValue)
            {
                claims.Add(new Claim(AybClaims.SteamId, user.SteamId.ToString()));
            }
            return claims;
        }

        private JwtSecurityToken CreateJwtTokenWithClaims(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(
              _authenticationSettings.JwtIssuer,
              _authenticationSettings.JwtIssuer,
              claims,
              expires: expires,
              signingCredentials: cred);

            return token;
        }
        private string WriteToken(JwtSecurityToken src)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(src);
        }
    }
}
