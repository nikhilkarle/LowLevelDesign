using System;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Specifications
{
    public class BooksByTitleSpecification : ISpecification<Book>
    {
        private readonly string _title;

        public BooksByTitleSpecification(string title)
        {
            _title = title;
        }

        public bool IsSatisfiedBy(Book entity)
        {
            return entity.Title.Contains(_title, StringComparison.OrdinalIgnoreCase);
        }
    }
}