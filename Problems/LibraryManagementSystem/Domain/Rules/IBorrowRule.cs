using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Rules
{
    public interface IBorrowRule
    {
        ValidationResult Validate(Member member, Book book);
    }
}