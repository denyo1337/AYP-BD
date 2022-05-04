using Application.Common;
using Application.DTO;
using Application.Functions.Queries.UsersQueries;
using FluentValidation;
namespace Infrastructure.Validators
{
    public class FriendsListQueryParamsValidator : AbstractValidator<FriendsListQueryParams>
    {
        private int[] allowedPageSizes = new[] { 10, 25, 50, 100 };
        private string[] allowedSortByColumnNames = { nameof(FriendDetailsDto.NickName).ToLower(), nameof(FriendDetailsDto.IsOnline).ToLower(), nameof(FriendDetailsDto.TimeCreated).ToLower()};

        public FriendsListQueryParamsValidator()
        {
            
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize musi zawierać się [{string.Join(",", allowedPageSizes)}]");
                    }
                });
            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"SortBy jest opcjonalne lub musi zawierać wartości  {string.Join(",", allowedSortByColumnNames)}");
        }
    }
}
