using Application.DTO;
using Application.Functions.Queries.UsersQueries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class GetUserFriendListsQueryValidator : AbstractValidator<GetUserFriendListsQuery>
    {
        private int[] allowedPageSizes = new[] { 10, 25, 50, 100 };
        private string[] allowedSortByColumnNames = { nameof(FriendDetailsDto.NickName), nameof(FriendDetailsDto.IsOnline), nameof(FriendDetailsDto.TimeCreated)};

        public GetUserFriendListsQueryValidator()
        {
            RuleFor(x => x.SteamID).NotEmpty();
            RuleFor(x => x.QueryParams.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.QueryParams.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize musi zawierać się [{string.Join(",", allowedPageSizes)}]");
                    }
                });
            RuleFor(x => x.QueryParams.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"SortBy jest opcjonalne lub musi zawierać wartości  {string.Join(",", allowedSortByColumnNames)}");
        }
    }
}
