using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Strategies
{
    public class RareBookLoanDurationStrategy : ILoanDurationStrategy
    {
        public int Priority => 100;

        public bool IsMatch(Member member, Book book)
        {
            return book.IsRare;
        }

        public int GetLoanDurationDays(Member member, Book book)
        {
            return 3;
        }
    }
}