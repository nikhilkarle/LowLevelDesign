using OAS.Application.Interfaces;
using OAS.Domain.Entities;
using OAS.Domain.Models;
using OAS.Infrastructure.Repositories;

namespace OAS.Application.Services;
public class BiddingService : IBiddingService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IBidRepository _bidRepository;
    private readonly INotificationService _notificationService;

    public BiddingService(
        IAuctionRepository auctionRepository,
        IBidRepository bidRepository,
        INotificationService notificationService)
    {
        _auctionRepository = auctionRepository;
        _bidRepository = bidRepository;
        _notificationService = notificationService;
    }

    public Bid PlaceBid(Guid auctionId, Guid bidderId, decimal amount, DateTime nowUtc)
    {
        var auction = _auctionRepository.GetById(auctionId)
                      ?? throw new InvalidOperationException("Auction not found.");

        var previousHighest = auction.HighestBid;

        var bid = auction.PlaceBid(bidderId, amount, nowUtc);

        _bidRepository.Add(bid);
        _auctionRepository.Update(auction);

        if (previousHighest != null && previousHighest.BidderId != bidderId)
        {
            _notificationService.NotifyUser(
                previousHighest.BidderId,
                new NotificationMessage(
                    "You were outbid",
                    $"Another bidder placed a higher bid on '{auction.Item.Name}'."));
        }

        _notificationService.NotifyUser(
            auction.SellerId,
            new NotificationMessage(
                "New Bid Received",
                $"A new bid of {amount} was placed on '{auction.Item.Name}'."));

        return bid;
    }
}