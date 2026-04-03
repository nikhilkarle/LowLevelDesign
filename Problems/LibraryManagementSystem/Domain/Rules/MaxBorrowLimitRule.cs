using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Rules
{
    public class MaxBorrowLimitRule : IBorrowRule
    {
        private readonly int _maxLimit;

        public MaxBorrowLimitRule(int maxLimit)
        {
            _maxLimit = maxLimit;
        }

        public ValidationResult Validate(Member member, Book book)
        {
            return member.GetActiveLoanCount() < _maxLimit
                ? ValidationResult.Success()
                : ValidationResult.Failure($"Member cannot borrow more than {_maxLimit} books.");
        }
    }
}