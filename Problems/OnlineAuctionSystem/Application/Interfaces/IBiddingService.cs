using OAS.Domain.Entities;

namespace OAS.Application.Interfaces;
public interface IBiddingService
{
    Bid PlaceBid(Guid auctionId, Guid bidderId, decimal amount, DateTime nowUtc);
}