using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Strategies
{
    public class FacultyLoanDurationStrategy : ILoanDurationStrategy
    {
        public int Priority => 30;

        public bool IsMatch(Member member, Book book)
        {
            return member.MemberType == MemberType.Faculty;
        }

        public int GetLoanDurationDays(Member member, Book book)
        {
            return 30;
        }
    }
}