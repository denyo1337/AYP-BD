using Application.Functions.Commands.UserCommands;
using Domain.Models;
using FluentValidation;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class AssignSteamIdToUserCommandValidator : AbstractValidator<AssignSteamIdToUserCommand>
    {
        public AssignSteamIdToUserCommandValidator(AypDbContext dbContext)
        {
            RuleFor(x => x.SteamId)
                .Custom((value, context) =>
                {
                    if (dbContext.Users.Any(x => x.SteamId == value))
                    {
                        context.AddFailure(nameof(User.SteamId), $"SteamId: {value} is already taken.");
                    }
                });
        }
    }
}
