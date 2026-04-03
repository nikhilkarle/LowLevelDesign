using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Rules
{
    public class OutstandingFineRule : IBorrowRule
    {
        public ValidationResult Validate(Member member, Book book)
        {
            return member.OutstandingFines > 0
                ? ValidationResult.Failure("Member has outstanding fines.")
                : ValidationResult.Success();
        }
    }
}