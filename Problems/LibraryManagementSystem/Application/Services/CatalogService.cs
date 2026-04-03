using System;
using System.Collections.Generic;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Specifications;

namespace LibraryManagementSystem.Application.Services
{
    public class CatalogService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddBook(Book book)
        {
            _bookRepository.Add(book);
            _unitOfWork.Commit();
        }

        public void UpdateBook(Guid id, string title, string author, string isbn, int publicationYear, bool isRare)
        {
            var book = _bookRepository.GetById(id)
                ?? throw new InvalidOperationException("Book not found.");

            book.UpdateDetails(title, author, isbn, publicationYear, isRare);
            _bookRepository.Update(book);
            _unitOfWork.Commit();
        }

        public void RemoveBook(Guid id)
        {
            var book = _bookRepository.GetById(id)
                ?? throw new InvalidOperationException("Book not found.");

            book.Remove();
            _bookRepository.Update(book);
            _unitOfWork.Commit();
        }

        public Book? GetBook(Guid id)
        {
            return _bookRepository.GetById(id);
        }

        public IReadOnlyCollection<Book> Search(ISpecification<Book> specification)
        {
            return _bookRepository.Find(specification);
        }
    }
}