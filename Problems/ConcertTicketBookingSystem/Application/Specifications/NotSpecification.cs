namespace ConcertTicketBookingSystem.Application.Specifications;

public class NotSpecification<T>(ISpecification<T> inner) : Specification<T>
{
    public override bool IsSatisfiedBy(T entity) => !inner.IsSatisfiedBy(entity);
}
