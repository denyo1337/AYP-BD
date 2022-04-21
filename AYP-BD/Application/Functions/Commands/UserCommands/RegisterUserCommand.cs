using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            return false;
        }
    }
}
