using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Strategies;

namespace LibraryManagementSystem.Application.Resolvers
{
    public class LoanDurationStrategyResolver : ILoanDurationStrategyResolver
    {
        private readonly IEnumerable<ILoanDurationStrategy> _strategies;

        public LoanDurationStrategyResolver(IEnumerable<ILoanDurationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public ILoanDurationStrategy Resolve(Member member, Book book)
        {
            var strategy = _strategies
                .Where(s => s.IsMatch(member, book))
                .OrderByDescending(s => s.Priority)
                .FirstOrDefault();

            if (strategy == null)
                throw new InvalidOperationException("No matching loan duration strategy found.");

            return strategy;
        }
    }
}