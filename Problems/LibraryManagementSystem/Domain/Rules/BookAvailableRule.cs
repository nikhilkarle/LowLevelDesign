using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Rules
{
    public class BookAvailableRule : IBorrowRule
    {
        public ValidationResult Validate(Member member, Book book)
        {
            return book.Status == BookStatus.Available
                ? ValidationResult.Success()
                : ValidationResult.Failure("Book is not available.");
        }
    }
}