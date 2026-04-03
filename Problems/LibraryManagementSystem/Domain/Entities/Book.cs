using System;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int PublicationYear { get; private set; }
        public bool IsRare { get; private set; }
        public BookStatus Status { get; private set; }

        public Book(Guid id, string title, string author, string isbn, int publicationYear, bool isRare)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            IsRare = isRare;
            Status = BookStatus.Available;
        }

        public void UpdateDetails(string title, string author, string isbn, int publicationYear, bool isRare)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            IsRare = isRare;
        }

        public void MarkBorrowed()
        {
            if (Status != BookStatus.Available)
                throw new InvalidOperationException("Book is not available for borrowing.");

            Status = BookStatus.Borrowed;
        }

        public void MarkReturned()
        {
            if (Status != BookStatus.Borrowed)
                throw new InvalidOperationException("Only borrowed books can be returned.");

            Status = BookStatus.Available;
        }

        public void Remove()
        {
            if (Status == BookStatus.Borrowed)
                throw new InvalidOperationException("Borrowed book cannot be removed.");

            Status = BookStatus.Removed;
        }
    }
}