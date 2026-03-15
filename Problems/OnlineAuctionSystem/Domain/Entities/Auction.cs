using OAS.Entities.Enums;

namespace OAS.Domain.Entities;

public class Auction
{
    private readonly List<Bid> _bids = new();
    private readonly HashSet<Guid> _watchers = new();
    private readonly object _bidLock = new();

    public Guid Id { get; }
    public Guid SellerId { get; }
    public Item Item { get; }
    public decimal StartingPrice { get; }
    public DateTime StartTimeUtc { get; }
    public DateTime EndTimeUtc { get; private set; }
    public AuctionStatus Status { get; private set; }

    public IReadOnlyList<Bid> Bids => _bids.AsReadOnly();
    public Bid? HighestBid => _bids.Count == 0 ? null : _bids.OrderByDescending(b => b.Amount).ThenBy(b => b.CreatedAtUtc).First();

    public Auction(Guid id, Guid sellerId, Item item, decimal startingPrice, DateTime startTimeUtc, DateTime endTimeUtc)
    {
        if (endTimeUtc <= startTimeUtc)
            throw new ArgumentException("End time must be after start time.");

        Id = id;
        SellerId = sellerId;
        Item = item;
        StartingPrice = startingPrice;
        StartTimeUtc = startTimeUtc;
        EndTimeUtc = endTimeUtc;
        Status = AuctionStatus.Draft;
    }

    public void Activate()
    {
        if (Status != AuctionStatus.Draft)
            throw new InvalidOperationException("Only draft auction can be activated.");

        Status = AuctionStatus.Active;
    }

    public void Close()
    {
        if (Status != AuctionStatus.Active)
            throw new InvalidOperationException("Only active auction can be closed.");

        Status = AuctionStatus.Closed;
    }

    public void Cancel()
    {
        if (Status == AuctionStatus.Closed)
            throw new InvalidOperationException("Closed auction cannot be cancelled.");

        Status = AuctionStatus.Cancelled;
    }

    public void Subscribe(Guid userId)
    {
        _watchers.Add(userId);
    }

    public IReadOnlyCollection<Guid> GetSubscribers() => _watchers.ToList().AsReadOnly();

    public Bid PlaceBid(Guid bidderId, decimal amount, DateTime nowUtc)
    {
        lock (_bidLock)
        {
            if (Status != AuctionStatus.Active)
                throw new InvalidOperationException("Auction is not active.");

            if (nowUtc >= EndTimeUtc)
                throw new InvalidOperationException("Auction has ended.");

            var minimumAllowed = HighestBid?.Amount + 1 ?? StartingPrice;

            if (amount < minimumAllowed)
                throw new InvalidOperationException($"Bid must be at least {minimumAllowed}.");

            var bid = new Bid(Guid.NewGuid(), Id, bidderId, amount, nowUtc);
            _bids.Add(bid);
            _watchers.Add(bidderId);

            return bid;
        }
    }
}