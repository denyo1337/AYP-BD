using Application.DTO;
using Application.Services;
using Domain.Data.Interfaces;
using MediatR;

namespace Application.Functions.Queries.UsersQueries
{
    public class GetUserDetailsQuery : IRequest<AccountDetailsDto>
    {
    }
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, AccountDetailsDto>
    {
        private readonly IUserContextService _userContext;
        private readonly IUsersRepostiory _usersRepostiory;
        public GetUserDetailsQueryHandler(IUserContextService userContext, IUsersRepostiory usersRepostiory)
        {
            _userContext = userContext;
            _usersRepostiory = usersRepostiory;
        }
        public async Task<AccountDetailsDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId;
            if (userId == null)
            {
                return null;
            }

            var user = await _usersRepostiory.GetAccountDetails((long)userId, cancellationToken);

            return  user is null ? null : new(user);
        }
    }
}
