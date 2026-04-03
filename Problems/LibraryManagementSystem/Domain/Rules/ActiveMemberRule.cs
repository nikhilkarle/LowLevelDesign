using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Rules
{
    public class ActiveMemberRule : IBorrowRule
    {
        public ValidationResult Validate(Member member, Book book)
        {
            return member.IsActive
                ? ValidationResult.Success()
                : ValidationResult.Failure("Member is not active.");
        }
    }
}