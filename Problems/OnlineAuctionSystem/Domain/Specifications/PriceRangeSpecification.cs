using OAS.Application.Interfaces;
using OAS.Domain.Entities;

namespace OAS.Domain.Specifications;
public class PriceRangeSpecification : ISpecification<Auction>
{
    private readonly decimal? _min;
    private readonly decimal? _max;

    public PriceRangeSpecification(decimal? min, decimal? max)
    {
        _min = min;
        _max = max;
    }

    public bool IsSatisfiedBy(Auction auction)
    {
        var price = auction.HighestBid?.Amount ?? auction.StartingPrice;

        if (_min.HasValue && price < _min.Value) return false;
        if (_max.HasValue && price > _max.Value) return false;

        return true;
    }
}