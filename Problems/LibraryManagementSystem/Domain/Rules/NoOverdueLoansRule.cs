using System;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Rules
{
    public class NoOverdueLoansRule : IBorrowRule
    {
        public ValidationResult Validate(Member member, Book book)
        {
            return member.HasOverdueLoans(DateTime.UtcNow)
                ? ValidationResult.Failure("Member has overdue loans.")
                : ValidationResult.Success();
        }
    }
}