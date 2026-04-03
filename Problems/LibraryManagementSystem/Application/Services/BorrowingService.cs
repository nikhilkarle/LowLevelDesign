using System;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Resolvers;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Rules;

namespace LibraryManagementSystem.Application.Services
{
    public class BorrowingService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IBorrowRule _borrowRule;
        private readonly ILoanDurationStrategyResolver _strategyResolver;
        private readonly ILoanFactory _loanFactory;
        private readonly IUnitOfWork _unitOfWork;

        public BorrowingService(
            IBookRepository bookRepository,
            IMemberRepository memberRepository,
            ILoanRepository loanRepository,
            IBorrowRule borrowRule,
            ILoanDurationStrategyResolver strategyResolver,
            ILoanFactory loanFactory,
            IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _loanRepository = loanRepository;
            _borrowRule = borrowRule;
            _strategyResolver = strategyResolver;
            _loanFactory = loanFactory;
            _unitOfWork = unitOfWork;
        }

        public Loan BorrowBook(Guid memberId, Guid bookId, DateTime borrowDate)
        {
            var member = _memberRepository.GetById(memberId)
                ?? throw new InvalidOperationException("Member not found.");

            var book = _bookRepository.GetById(bookId)
                ?? throw new InvalidOperationException("Book not found.");

            foreach (var l in _loanRepository.GetLoansByMember(memberId))
            {
                l.UpdateOverdueStatus(DateTime.UtcNow);
            }

            var validation = _borrowRule.Validate(member, book);
            if (!validation.IsValid)
                throw new InvalidOperationException(string.Join(" | ", validation.Errors));

            var strategy = _strategyResolver.Resolve(member, book);
            var loanDurationDays = strategy.GetLoanDurationDays(member, book);

            var loan = _loanFactory.Create(member, book, borrowDate, loanDurationDays);

            book.MarkBorrowed();
            member.AddLoanToHistory(loan);

            _loanRepository.Add(loan);
            _bookRepository.Update(book);
            _memberRepository.Update(member);

            _unitOfWork.Commit();
            return loan;
        }

        public void ReturnBook(Guid memberId, Guid bookId, DateTime returnedAt)
        {
            var member = _memberRepository.GetById(memberId)
                ?? throw new InvalidOperationException("Member not found.");

            var book = _bookRepository.GetById(bookId)
                ?? throw new InvalidOperationException("Book not found.");

            var loan = _loanRepository.GetActiveLoan(bookId, memberId)
                ?? throw new InvalidOperationException("Active loan not found.");

            loan.UpdateOverdueStatus(returnedAt);
            loan.MarkReturned(returnedAt);
            book.MarkReturned();

            _loanRepository.Update(loan);
            _bookRepository.Update(book);
            _memberRepository.Update(member);

            _unitOfWork.Commit();
        }
    }
}