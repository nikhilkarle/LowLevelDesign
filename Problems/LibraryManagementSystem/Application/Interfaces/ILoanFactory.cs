using System;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface ILoanFactory
    {
        Loan Create(Member member, Book book, DateTime borrowDate, int durationDays);
    }
}