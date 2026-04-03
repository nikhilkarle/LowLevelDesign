using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Specifications
{
    public class AvailableBooksSpecification : ISpecification<Book>
    {
        public bool IsSatisfiedBy(Book entity)
        {
            return entity.Status == BookStatus.Available;
        }
    }
}