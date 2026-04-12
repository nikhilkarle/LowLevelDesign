namespace ConcertTicketBookingSystem.Application.Specifications;

public class AllSpecification<T> : Specification<T>
{
    public override bool IsSatisfiedBy(T entity) => true;
}
