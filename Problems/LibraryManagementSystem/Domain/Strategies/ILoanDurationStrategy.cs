using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Strategies
{
    public interface ILoanDurationStrategy
    {
        int Priority { get; }
        bool IsMatch(Member member, Book book);
        int GetLoanDurationDays(Member member, Book book);
    }
}