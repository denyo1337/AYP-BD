using Application.Services;
using Domain.Data.Interfaces;
using MediatR;

namespace Application.Functions.Queries.UsersQueries
{
    public class IsEmailTakenQuery : IRequest<bool>
    {
        public string Email { get; set; }
        public IsEmailTakenQuery(string email)
        {
            Email = email;
        }
    }
    public class IsEmailTakenQueryHandler : IRequestHandler<IsEmailTakenQuery, bool>
    {
        private readonly IUsersRepostiory _usersRepostiory;
        private readonly IUserContextService _userContext;

        public IsEmailTakenQueryHandler(IUsersRepostiory repostiory, IUserContextService userContext)
        {
            _usersRepostiory = repostiory;
            _userContext = userContext;
        }
        public async Task<bool> Handle(IsEmailTakenQuery request, CancellationToken cancellationToken)
        {
            if (_userContext.GetUserId.HasValue)
                return await _usersRepostiory.IsEmailTaken(request.Email, _userContext.GetUserId.Value, cancellationToken);
            else
                return await _usersRepostiory.IsEmailTaken(request.Email, cancellationToken);
        }
    }
}
