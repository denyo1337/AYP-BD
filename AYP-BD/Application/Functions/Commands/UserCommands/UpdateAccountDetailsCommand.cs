using Application.Interfaces;
using Application.Services;
using Domain.Data.Interfaces;
using MediatR;

namespace Application.Functions.Commands.UserCommands
{
    public class UpdateAccountDetailsCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string SteamNickName { get; set; }
    }
    public class UpdateAccountDetailsCommandHandler : IRequestHandler<UpdateAccountDetailsCommand, bool>
    {
        private readonly IUserContextService _userContext;
        private readonly IUsersRepostiory _usersRepostiory;
        private readonly IHttpRequestHandler _httpHandler;

        public UpdateAccountDetailsCommandHandler(IUserContextService userContext, IUsersRepostiory usersRepostiory, IHttpRequestHandler httpRequestHandler)
        {
            _userContext = userContext;
            _usersRepostiory = usersRepostiory;
            _httpHandler = httpRequestHandler;
        }
        public async Task<bool> Handle(UpdateAccountDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepostiory.GetAccountDetails((long)_userContext.GetUserId, cancellationToken);

            if (user == null)
                return false;

            _ = await _httpHandler.Get<object>("d", new { i = true });



            user.Update(request.Email, request.NickName, request.PhoneNumber, request.Nationality);


            return await _usersRepostiory.SaveChangesAsync(cancellationToken);
        }
    }

}
