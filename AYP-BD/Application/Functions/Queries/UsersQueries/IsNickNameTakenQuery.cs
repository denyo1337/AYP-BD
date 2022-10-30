using Application.Services;
using Domain.Data.Interfaces;
using MediatR;

namespace Application.Functions.Queries.UsersQueries
{
    public class IsNickNameTakenQuery : IRequest<bool>
    {
        public string NickName { get; set; }

        public IsNickNameTakenQuery(string nickName)
        {
            NickName = nickName;
        }
    }
    public class IsNickNameTakenQueryHandler : IRequestHandler<IsNickNameTakenQuery, bool>
    {
        private readonly IUsersRepostiory _usersRepostiory;
        private readonly IUserContextService _userContext;

        public IsNickNameTakenQueryHandler(IUsersRepostiory usersRepostiory, IUserContextService userContext)
        {
            _usersRepostiory = usersRepostiory;
            _userContext = userContext;
        }
        public async Task<bool> Handle(IsNickNameTakenQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.NickName)) return true;
            return await _usersRepostiory.IsNickNameTaken(request.NickName, cancellationToken);
        }
    }
}
