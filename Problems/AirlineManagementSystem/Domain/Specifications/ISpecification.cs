namespace AirlineManagementSystem.Domain.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T item);
}