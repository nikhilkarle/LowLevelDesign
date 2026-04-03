using System;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; }
        public Guid BookId { get; }
        public Guid MemberId { get; }
        public DateTime BorrowDate { get; }
        public DateTime DueDate { get; }
        public DateTime? ReturnDate { get; private set; }
        public LoanStatus Status { get; private set; }

        public Loan(Guid id, Guid bookId, Guid memberId, DateTime borrowDate, DateTime dueDate)
        {
            Id = id;
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            Status = LoanStatus.Active;
        }

        public void MarkReturned(DateTime returnedAt)
        {
            if (Status != LoanStatus.Active && Status != LoanStatus.Overdue)
                throw new InvalidOperationException("Only active or overdue loans can be returned.");

            ReturnDate = returnedAt;
            Status = LoanStatus.Returned;
        }

        public bool IsOverdue(DateTime now)
        {
            return (Status == LoanStatus.Active || Status == LoanStatus.Overdue) && now.Date > DueDate.Date;
        }

        public void UpdateOverdueStatus(DateTime now)
        {
            if (IsOverdue(now))
                Status = LoanStatus.Overdue;
        }
    }
}