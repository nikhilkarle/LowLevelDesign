namespace ConcertTicketBookingSystem.Application.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract bool IsSatisfiedBy(T entity);

    public ISpecification<T> And(ISpecification<T> other) => new AndSpecification<T>(this, other);
    public ISpecification<T> Or(ISpecification<T> other)  => new OrSpecification<T>(this, other);
    public ISpecification<T> Not()                        => new NotSpecification<T>(this);
}
