using Application.Interfaces;
using Application.Services;
using Domain.Data.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions.Commands.UserCommands
{
    public class AssignSteamIdToUserCommand : IRequest<bool>
    {
        public long? SteamId { get; set; }
        public bool? ResetValue { get; set; } = false;

    }
    public class AssignSteamToUserCommandHanlder : IRequestHandler<AssignSteamIdToUserCommand, bool>
    {
        private readonly IUserContextService _userContext;
        private readonly IUsersRepostiory _usersRepostiory;

        public AssignSteamToUserCommandHanlder(IUserContextService userContext, IUsersRepostiory usersRepostiory)
        {
            _userContext = userContext;
            _usersRepostiory = usersRepostiory;
        }

        public async Task<bool> Handle(AssignSteamIdToUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId;

            var user = await _usersRepostiory.GetAsync(userId!.Value, cancellationToken);
            if (user == null)
                return false;

            user.UpdateSteamId(request.SteamId, request.ResetValue);

            return await _usersRepostiory.SaveChangesAsync(cancellationToken); ;
        }
    }
}
