using Application.Services;
using Domain.Data.Interfaces;
using Domain.Enums;
using Domain.Models;
using MediatR;

namespace Application.Functions.Commands.UserCommands
{
    public class AssignSteamIdToUserCommand : IRequest<SteamIdValidationResult>
    {
        public long? SteamId { get; set; }
        public bool? ResetValue { get; set; } = false;

    }
    public class AssignSteamToUserCommandHanlder : IRequestHandler<AssignSteamIdToUserCommand, SteamIdValidationResult>
    {
        private readonly IUserContextService _userContext;
        private readonly IUsersRepostiory _usersRepostiory;
        private const int MinDaysToUpdate = 7;
        public AssignSteamToUserCommandHanlder(IUserContextService userContext, IUsersRepostiory usersRepostiory)
        {
            _userContext = userContext;
            _usersRepostiory = usersRepostiory;
        }

        public async Task<SteamIdValidationResult> Handle(AssignSteamIdToUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId;
            var user = await _usersRepostiory.GetAsync(userId!.Value, cancellationToken);
            if (user == null)
                return SteamIdValidationResult.DoestNotExist;
            if (CheckIfPeriodIsInvalid(user))
                return SteamIdValidationResult.ErrorOnPeriod;

            user.UpdateSteamId(request.SteamId, request.ResetValue);
            var isSaved = await _usersRepostiory.SaveChangesAsync(cancellationToken);
            return isSaved ? SteamIdValidationResult.Ok : SteamIdValidationResult.Error;
        }
        private static bool CheckIfPeriodIsInvalid(User user)
        {
            return user.LastSteamIdUpdate.HasValue &&
                user.LastSteamIdUpdate.Value.AddDays(MinDaysToUpdate).Date >= DateTime.Now.Date;
        }
    }
}
