using OAS.Domain.Entities;

namespace OAS.Infrastructure.Repositories;

public class InMemoryBidRepository : IBidRepository
{
    private readonly List<Bid> _bids = new();

    public void Add(Bid bid) => _bids.Add(bid);

    public IEnumerable<Bid> GetByAuctionId(Guid auctionId) =>
        _bids.Where(b => b.AuctionId == auctionId);
}