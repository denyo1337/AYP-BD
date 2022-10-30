using Application.DTO;
using Application.Services;
using Domain.Data.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Functions.Commands.UserCommands
{
    public class UpdateAccountDetailsCommand : IRequest<AccountDetailsDto>
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string Password { get; set; }
    }
    public class UpdateAccountDetailsCommandHandler : IRequestHandler<UpdateAccountDetailsCommand, AccountDetailsDto>
    {
        private readonly IUserContextService _userContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUsersRepostiory _usersRepostiory;

        public UpdateAccountDetailsCommandHandler(IUserContextService userContext, IUsersRepostiory usersRepostiory, IPasswordHasher<User> passwordHasher)
        {
            _userContext = userContext;
            _usersRepostiory = usersRepostiory;
            _passwordHasher = passwordHasher;
        }
        public async Task<AccountDetailsDto> Handle(UpdateAccountDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepostiory.GetAccountDetails((long)_userContext.GetUserId, cancellationToken);

            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed) return null;

            user.Update(request.Email, request.NickName, request.PhoneNumber, request.Nationality);

            var isChanged = await _usersRepostiory.SaveChangesAsync(cancellationToken);

            return isChanged ? new(user) : null;
        }
    }

}
