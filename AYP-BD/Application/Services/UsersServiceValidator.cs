using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUsersServiceValidator
    {
        LoginVerificationResult ValidateUserDataAndPassword(User user, string requestPassword);
    }
    public class UsersServiceValidator : IUsersServiceValidator
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UsersServiceValidator(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public LoginVerificationResult ValidateUserDataAndPassword(User user, string requestPassword)
        {
            if (user == null)
                return LoginVerificationResult.WrongPassword;
            if (user.IsBanned.HasValue && user.IsBanned.Value)
                return LoginVerificationResult.UserBanned;
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, requestPassword);
            if (result == PasswordVerificationResult.Failed)
                return LoginVerificationResult.WrongPassword;

            return LoginVerificationResult.Succes;
        }
    }
}
