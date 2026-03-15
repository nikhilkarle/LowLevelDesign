namespace OAS.Domain.Entities;

public class Bid
{
    public Guid Id { get; }
    public Guid AuctionId { get; }
    public Guid BidderId { get; }
    public decimal Amount { get; }
    public DateTime CreatedAtUtc { get; }

    public Bid(Guid id, Guid auctionId, Guid bidderId, decimal amount, DateTime createdAtUtc)
    {
        Id = id;
        AuctionId = auctionId;
        BidderId = bidderId;
        Amount = amount;
        CreatedAtUtc = createdAtUtc;
    }
}