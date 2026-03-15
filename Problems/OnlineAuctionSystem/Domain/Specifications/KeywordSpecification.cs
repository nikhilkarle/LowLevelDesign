using OAS.Application.Interfaces;
using OAS.Domain.Entities;

namespace OAS.Domain.Specifications;

public class KeywordSpecification : ISpecification<Auction>
{
    private readonly string _keyword;

    public KeywordSpecification(string keyword)
    {
        _keyword = keyword.ToLower();
    }

    public bool IsSatisfiedBy(Auction auction)
    {
        return auction.Item.Name.ToLower().Contains(_keyword) ||
               auction.Item.Description.ToLower().Contains(_keyword);
    }
}