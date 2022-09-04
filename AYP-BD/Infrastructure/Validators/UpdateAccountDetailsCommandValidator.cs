using Application.Functions.Commands.UserCommands;
using Application.Services;
using Domain.Models;
using FluentValidation;
using Infrastructure.Data;

namespace Infrastructure.Validators
{
    public class UpdateAccountDetailsCommandValidator : AbstractValidator<UpdateAccountDetailsCommand>
    {

        public UpdateAccountDetailsCommandValidator(AypDbContext _dbContext, IUserContextService _userContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.NickName)
               .MinimumLength(3)
               .Custom((value, context) =>
               {
                   var isTaken = _dbContext.Users.Any(x => x.NickName == value && x.Id != _userContext.GetUserId.Value);
                   if (isTaken)
                   {
                       context.AddFailure(nameof(User.NickName), $"Nick {value} jest zajęty");
                   }
               });
            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress()
               .Custom((value, context) =>
               {
                   var isTaken = _dbContext.Users.Any(x => x.Email == value && x.Id != _userContext.GetUserId.Value);
                   if (isTaken)
                   {
                       context.AddFailure(nameof(User.Email), $"Email : {value} is taken ");
                   }
               });
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
