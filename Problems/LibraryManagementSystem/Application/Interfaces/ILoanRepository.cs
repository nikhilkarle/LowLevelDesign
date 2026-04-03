using System;
using System.Collections.Generic;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface ILoanRepository
    {
        void Add(Loan loan);
        void Update(Loan loan);
        Loan? GetActiveLoan(Guid bookId, Guid memberId);
        IReadOnlyCollection<Loan> GetLoansByMember(Guid memberId);
        IReadOnlyCollection<Loan> GetAll();
    }
}