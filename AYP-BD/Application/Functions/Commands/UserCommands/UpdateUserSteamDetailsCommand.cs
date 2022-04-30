using Application.Common;
using Application.DTO;
using Application.Functions.Commands.UserCommands;
using Application.Interfaces;
using Application.Services;
using Domain.Data.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Functions.Commands.UserCommands
{
    public class UpdateUserSteamDetailsCommand : IRequest<bool>
    {

    }
}
public class UpdateUserSteamDetailsCommandHandler : IRequestHandler<UpdateUserSteamDetailsCommand, bool>
{
    private readonly IUserContextService _userContext;
    private readonly IUsersRepostiory _usersRepostiory;
    private readonly IHttpRequestHandler _httpHandler;
    private readonly IEntityGenerator _entityGenerator;
    private const string USER_DETAILS_PATH = "ISteamUser/GetPlayerSummaries/v2/";
    public UpdateUserSteamDetailsCommandHandler(IUserContextService userContext, IUsersRepostiory usersRepostiory, IHttpRequestHandler httpHandler, IEntityGenerator entityGenerator)
    {
        _userContext = userContext;
        _usersRepostiory = usersRepostiory;
        _httpHandler = httpHandler;
        _entityGenerator = entityGenerator;
    }

    public async Task<bool> Handle(UpdateUserSteamDetailsCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId;
        if (userId == null) return false;
        var user = await _usersRepostiory.GetAccountDetailsWithSteamUserData(userId.Value, cancellationToken);

        if (user == null) return false;

        var userSteamData = await _httpHandler.Get<UserSteamDtaDto>(USER_DETAILS_PATH, new { steamIds = user.SteamId });

        if (userSteamData.StatusCode != StatusCodes.Status200OK || userSteamData.Model is null) return false;

        var data = userSteamData.Model.Response.Players.FirstOrDefault();

        if (user.SteamUserData == null)
        {
            user.SteamUserData = new(
                _entityGenerator.Generate(),
                data.SteamNickName,
                data.ProfileUrl,
                data.AvatarfullUrl,
                data.RealName,
                data.AccountCreated,
                data.SteamNationality);
        }
        else
        {
           user.SteamUserData.UpdateSteamUserData(
               data.SteamNickName,
               data.ProfileUrl,
               data.AvatarfullUrl,
               data.RealName,
               data.AccountCreated,
               data.SteamNationality);
        }

        return await _usersRepostiory.SaveChangesAsync(cancellationToken);
    }
}
