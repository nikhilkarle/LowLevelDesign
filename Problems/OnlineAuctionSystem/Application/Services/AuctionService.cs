using OAS.Application.Interfaces;
using OAS.Domain.Entities;
using OAS.Domain.Models;
using OAS.Entities.Enums;
using OAS.Infrastructure.Repositories;

namespace OAS.Application.Services;
public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly INotificationService _notificationService;

    public AuctionService(IAuctionRepository auctionRepository, INotificationService notificationService)
    {
        _auctionRepository = auctionRepository;
        _notificationService = notificationService;
    }

    public Auction CreateAuction(Guid sellerId, string itemName, string description, string category,
        decimal startingPrice, DateTime startTimeUtc, DateTime endTimeUtc)
    {
        var item = new Item(Guid.NewGuid(), itemName, description, category);
        var auction = new Auction(Guid.NewGuid(), sellerId, item, startingPrice, startTimeUtc, endTimeUtc);
        _auctionRepository.Add(auction);
        return auction;
    }

    public void ActivateAuction(Guid auctionId)
    {
        var auction = _auctionRepository.GetById(auctionId)
                      ?? throw new InvalidOperationException("Auction not found.");

        auction.Activate();
        _auctionRepository.Update(auction);
    }

    public void CloseExpiredAuctions(DateTime nowUtc)
    {
        var activeAuctions = _auctionRepository.GetAll()
            .Where(a => a.Status == AuctionStatus.Active && nowUtc >= a.EndTimeUtc)
            .ToList();

        foreach (var auction in activeAuctions)
        {
            auction.Close();
            _auctionRepository.Update(auction);

            var winner = auction.HighestBid;
            if (winner != null)
            {
                _notificationService.NotifyUser(
                    winner.BidderId,
                    new NotificationMessage(
                        "Auction Won",
                        $"You won auction '{auction.Item.Name}' with bid {winner.Amount}."));
            }

            _notificationService.NotifyUser(
                auction.SellerId,
                new NotificationMessage(
                    "Auction Closed",
                    $"Auction '{auction.Item.Name}' has closed."));
        }
    }
}