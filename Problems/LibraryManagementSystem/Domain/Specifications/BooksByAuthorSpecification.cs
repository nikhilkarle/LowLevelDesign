using System;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Specifications
{
    public class BooksByAuthorSpecification : ISpecification<Book>
    {
        private readonly string _author;

        public BooksByAuthorSpecification(string author)
        {
            _author = author;
        }

        public bool IsSatisfiedBy(Book entity)
        {
            return entity.Author.Contains(_author, StringComparison.OrdinalIgnoreCase);
        }
    }
}