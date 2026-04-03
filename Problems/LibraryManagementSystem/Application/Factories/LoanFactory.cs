using System;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Factories
{
    public class LoanFactory : ILoanFactory
    {
        public Loan Create(Member member, Book book, DateTime borrowDate, int durationDays)
        {
            var dueDate = borrowDate.Date.AddDays(durationDays);
            return new Loan(Guid.NewGuid(), book.Id, member.Id, borrowDate, dueDate);
        }
    }
}