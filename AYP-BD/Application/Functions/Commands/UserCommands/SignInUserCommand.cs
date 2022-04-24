using Application.Common;
using Application.DTO;
using Domain.Data.Interfaces;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions.Commands.UserCommands
{
    public class SignInUserCommand : IRequest<SignInResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInResultDto>
    {
        private readonly IUsersRepostiory _usersRepostiory;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public SignInUserCommandHandler(IUsersRepostiory usersRepostiory, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _usersRepostiory = usersRepostiory;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public async Task<SignInResultDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepostiory.GetUser(request.Email, cancellationToken);
            if (user == null)
                return new(LoginVerificationResult.WrongPassword);
            if (user.IsBanned.HasValue && user.IsBanned.Value)
                return new(LoginVerificationResult.UserBanned);
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return new(LoginVerificationResult.WrongPassword);

            List<Claim> claims = new()
            {
                new Claim(AybClaims.UserId, user.Id.ToString()),
                new Claim(AybClaims.EmailAddress, user.Email),
                new Claim(AybClaims.NickName, user.NickName),
                new Claim(AybClaims.Role, user.Role.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(
              _authenticationSettings.JwtIssuer,
              _authenticationSettings.JwtIssuer,
              claims,
              expires: expires,
              signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            user.LastLogOn = DateTime.Now;

            await _usersRepostiory.SaveChangesAsync(cancellationToken);

            return new(tokenHandler.WriteToken(token), LoginVerificationResult.Succes);
        }
    }
}
