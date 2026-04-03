using System;
using System.Collections.Generic;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Specifications;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IBookRepository
    {
        void Add(Book book);
        void Update(Book book);
        void Remove(Guid bookId);
        Book? GetById(Guid id);
        IReadOnlyCollection<Book> GetAll();
        IReadOnlyCollection<Book> Find(ISpecification<Book> specification);
    }
}