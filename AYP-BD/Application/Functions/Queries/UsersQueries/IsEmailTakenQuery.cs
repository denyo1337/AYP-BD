﻿using Domain.Data.Interfaces;
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
        public IsEmailTakenQueryHandler(IUsersRepostiory repostiory)
        {
            _usersRepostiory = repostiory;
        }
        public async Task<bool> Handle(IsEmailTakenQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepostiory.IsEmailTaken(request.Email, cancellationToken);
        }
    }
}
