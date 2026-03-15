using OAS.Application.Interfaces;
using OAS.Application.Services;
using OAS.Domain.Models;
using OAS.Entities.Enums;
using OAS.Infrastructure.Notifications;
using OAS.Infrastructure.Repositories;

class Program
{
    static void Main(string[] args)
    {
        IUserRepository userRepo = new InMemoryUserRepository();
        IAuctionRepository auctionRepo = new InMemoryAuctionRepository();
        IBidRepository bidRepo = new InMemoryBidRepository();
        INotificationService notificationService = new ConsoleNotificationService();

        IAuthenticationService authService = new AuthenticationService(userRepo);
        IAuctionService auctionService = new AuctionService(auctionRepo, notificationService);
        IBiddingService biddingService = new BiddingService(auctionRepo, bidRepo, notificationService);
        ISearchService searchService = new SearchService(auctionRepo);

        var seller = authService.Register("seller1", "seller@test.com", "pass123");
        var bidder1 = authService.Register("bidder1", "bidder1@test.com", "pass123");
        var bidder2 = authService.Register("bidder2", "bidder2@test.com", "pass123");

        var auction = auctionService.CreateAuction(
            seller.Id,
            "iPhone 15",
            "Brand new iPhone",
            "Electronics",
            500,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(30));

        auctionService.ActivateAuction(auction.Id);

        biddingService.PlaceBid(auction.Id, bidder1.Id, 550, DateTime.UtcNow);
        biddingService.PlaceBid(auction.Id, bidder2.Id, 600, DateTime.UtcNow);

        var results = searchService.Search(new AuctionSearchCriteria
        {
            Keyword = "iphone",
            Category = "Electronics",
            Status = AuctionStatus.Active
        });

        foreach (var a in results)
        {
            Console.WriteLine($"{a.Item.Name} - Current Price: {a.HighestBid?.Amount ?? a.StartingPrice}");
        }
    }
}