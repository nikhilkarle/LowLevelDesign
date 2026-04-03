using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Strategies;

namespace LibraryManagementSystem.Application.Resolvers
{
    public interface ILoanDurationStrategyResolver
    {
        ILoanDurationStrategy Resolve(Member member, Book book);
    }
}