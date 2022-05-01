using Application.Functions.Commands.UserCommands;
using Domain.Models;
using FluentValidation;
using Infrastructure.Data;

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
