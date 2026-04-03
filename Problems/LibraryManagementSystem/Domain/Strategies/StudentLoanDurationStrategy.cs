using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Strategies
{
    public class StudentLoanDurationStrategy : ILoanDurationStrategy
    {
        public int Priority => 20;

        public bool IsMatch(Member member, Book book)
        {
            return member.MemberType == MemberType.Student;
        }

        public int GetLoanDurationDays(Member member, Book book)
        {
            return 14;
        }
    }
}