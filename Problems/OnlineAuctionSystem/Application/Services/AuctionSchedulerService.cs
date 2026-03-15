using OAS.Application.Interfaces;

namespace OAS.Application.Services;
public class AuctionSchedulerService
{
    private readonly IAuctionService _auctionService;

    public AuctionSchedulerService(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    public void RunClosingCycle()
    {
        _auctionService.CloseExpiredAuctions(DateTime.UtcNow);
    }
}