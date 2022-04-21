using Application.Common;
using Domain.Data.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Functions.Commands.UserCommands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Natonality { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

  
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUsersRepostiory _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEntityGenerator _entityGenerator;


        public RegisterUserCommandHandler(IUsersRepostiory repostiory, IPasswordHasher<User> passwordHasher, IEntityGenerator entityGenerator)
        {
            _repository = repostiory;
            _passwordHasher = passwordHasher;
            _entityGenerator = entityGenerator;
        }
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Id = _entityGenerator.Generate(),
                Email = request.Email,
                NickName = request.NickName,
                Nationality = request.Natonality,
            };

            return await _repository.AddUser(user);
        }
    }
}
