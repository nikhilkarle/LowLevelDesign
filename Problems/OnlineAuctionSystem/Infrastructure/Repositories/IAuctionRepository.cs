using OAS.Domain.Entities;

namespace OAS.Infrastructure.Repositories;

public interface IAuctionRepository
{
    void Add(Auction auction);
    Auction? GetById(Guid id);
    IEnumerable<Auction> GetAll();
    void Update(Auction auction);
}