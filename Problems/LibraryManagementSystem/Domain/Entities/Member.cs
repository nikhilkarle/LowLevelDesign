using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Domain.ValueObjects;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Member
    {
        private readonly List<Loan> _borrowingHistory = new();

        public Guid Id { get; }
        public string Name { get; private set; }
        public ContactInfo ContactInfo { get; private set; }
        public MemberType MemberType { get; private set; }
        public bool IsActive { get; private set; }
        public decimal OutstandingFines { get; private set; }

        public IReadOnlyCollection<Loan> BorrowingHistory => _borrowingHistory.AsReadOnly();

        public Member(Guid id, string name, ContactInfo contactInfo, MemberType memberType)
        {
            Id = id;
            Name = name;
            ContactInfo = contactInfo;
            MemberType = memberType;
            IsActive = true;
            OutstandingFines = 0m;
        }

        public void UpdateDetails(string name, ContactInfo contactInfo, MemberType memberType)
        {
            Name = name;
            ContactInfo = contactInfo;
            MemberType = memberType;
        }

        public void AddLoanToHistory(Loan loan)
        {
            _borrowingHistory.Add(loan);
        }

        public int GetActiveLoanCount()
        {
            return _borrowingHistory.Count(l => l.Status == LoanStatus.Active);
        }

        public bool HasOverdueLoans(DateTime now)
        {
            return _borrowingHistory.Any(l => l.IsOverdue(now));
        }

        public void AddFine(decimal amount)
        {
            OutstandingFines += amount;
        }

        public void PayFine(decimal amount)
        {
            OutstandingFines -= amount;
            if (OutstandingFines < 0)
                OutstandingFines = 0;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}