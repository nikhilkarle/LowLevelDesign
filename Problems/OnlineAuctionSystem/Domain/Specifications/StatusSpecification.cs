using OAS.Application.Interfaces;
using OAS.Domain.Entities;
using OAS.Entities.Enums;

namespace OAS.Domain.Specifications;
public class StatusSpecification : ISpecification<Auction>
{
    private readonly AuctionStatus _status;

    public StatusSpecification(AuctionStatus status)
    {
        _status = status;
    }

    public bool IsSatisfiedBy(Auction auction)
    {
        return auction.Status == _status;
    }
}