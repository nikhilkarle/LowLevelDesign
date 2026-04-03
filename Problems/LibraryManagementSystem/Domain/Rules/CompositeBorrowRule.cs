using System.Collections.Generic;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Rules
{
    public class CompositeBorrowRule : IBorrowRule
    {
        private readonly IEnumerable<IBorrowRule> _rules;

        public CompositeBorrowRule(IEnumerable<IBorrowRule> rules)
        {
            _rules = rules;
        }

        public ValidationResult Validate(Member member, Book book)
        {
            var errors = new List<string>();

            foreach (var rule in _rules)
            {
                var result = rule.Validate(member, book);
                if (!result.IsValid)
                    errors.AddRange(result.Errors);
            }

            return errors.Count == 0
                ? ValidationResult.Success()
                : ValidationResult.Failure(errors);
        }
    }
}