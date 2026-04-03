using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Strategies
{
    public class DefaultLoanDurationStrategy : ILoanDurationStrategy
    {
        public int Priority => 1;

        public bool IsMatch(Member member, Book book)
        {
            return true;
        }

        public int GetLoanDurationDays(Member member, Book book)
        {
            return 7;
        }
    }
}