using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Specifications;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly Dictionary<Guid, Book> _books = new();

        public void Add(Book book)
        {
            _books[book.Id] = book;
        }

        public void Update(Book book)
        {
            _books[book.Id] = book;
        }

        public void Remove(Guid bookId)
        {
            _books.Remove(bookId);
        }

        public Book? GetById(Guid id)
        {
            return _books.TryGetValue(id, out var book) ? book : null;
        }

        public IReadOnlyCollection<Book> GetAll()
        {
            return _books.Values.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Book> Find(ISpecification<Book> specification)
        {
            return _books.Values.Where(specification.IsSatisfiedBy).ToList().AsReadOnly();
        }
    }
}