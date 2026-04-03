using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class InMemoryLoanRepository : ILoanRepository
    {
        private readonly Dictionary<Guid, Loan> _loans = new();

        public void Add(Loan loan)
        {
            _loans[loan.Id] = loan;
        }

        public void Update(Loan loan)
        {
            _loans[loan.Id] = loan;
        }

        public Loan? GetActiveLoan(Guid bookId, Guid memberId)
        {
            return _loans.Values.FirstOrDefault(l =>
                l.BookId == bookId &&
                l.MemberId == memberId &&
                (l.Status == LoanStatus.Active || l.Status == LoanStatus.Overdue));
        }

        public IReadOnlyCollection<Loan> GetLoansByMember(Guid memberId)
        {
            return _loans.Values.Where(l => l.MemberId == memberId).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Loan> GetAll()
        {
            return _loans.Values.ToList().AsReadOnly();
        }
    }
}