using OAS.Domain.Entities;

namespace OAS.Application.Interfaces;
public interface IAuctionService
{
    Auction CreateAuction(Guid sellerId, string itemName, string description, string category,
        decimal startingPrice, DateTime startTimeUtc, DateTime endTimeUtc);

    void ActivateAuction(Guid auctionId);
    void CloseExpiredAuctions(DateTime nowUtc);
}