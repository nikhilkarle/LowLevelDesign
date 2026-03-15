using OAS.Domain.Entities;

namespace OAS.Infrastructure.Repositories;

public interface IBidRepository
{
    void Add(Bid bid);
    IEnumerable<Bid> GetByAuctionId(Guid auctionId);
}