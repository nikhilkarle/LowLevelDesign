using OAS.Application.Interfaces;
using OAS.Domain.Entities;

namespace OAS.Domain.Specifications;
public class CategorySpecification : ISpecification<Auction>
{
    private readonly string _category;

    public CategorySpecification(string category)
    {
        _category = category.ToLower();
    }

    public bool IsSatisfiedBy(Auction auction)
    {
        return auction.Item.Category.ToLower() == _category;
    }
}