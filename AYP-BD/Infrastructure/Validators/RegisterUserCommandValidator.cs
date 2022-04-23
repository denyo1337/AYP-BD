using Application.Functions.Commands.UserCommands;
using Domain.Models;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(AypDbContext _dbContext)
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .Custom((value, context) =>
               {
                   var isTaken = _dbContext.Users.Any(x => x.Email == value);
                   if (isTaken)
                   {
                       context.AddFailure(nameof(User.Email), $"Email : {value} is taken ");
                   }
               });
            RuleFor(x => x.Password)
               .MinimumLength(6)
               .Equal(e => e.ConfirmPassword);
            RuleFor(x => x.NickName)
                .MinimumLength(3)
                .Custom((value, context) =>
                {
                    var isTaken = _dbContext.Users.Any(x => x.NickName == value);
                    if (isTaken)
                    {
                        context.AddFailure($"Nick {value} jest zajęty");
                    }
                });
            RuleFor(x => x.Natonality)
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}
