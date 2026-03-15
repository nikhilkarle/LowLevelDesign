using OAS.Domain.Entities;

namespace OAS.Infrastructure.Repositories;

public class InMemoryAuctionRepository : IAuctionRepository
{
    private readonly Dictionary<Guid, Auction> _auctions = new();

    public void Add(Auction auction) => _auctions[auction.Id] = auction;

    public Auction? GetById(Guid id) => _auctions.TryGetValue(id, out var auction) ? auction : null;

    public IEnumerable<Auction> GetAll() => _auctions.Values;

    public void Update(Auction auction) => _auctions[auction.Id] = auction;
}