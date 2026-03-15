using OAS.Entities.Enums;

namespace OAS.Domain.Models;

public class AuctionSearchCriteria
{
    public string? Keyword { get; set; }
    public string? Category { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public AuctionStatus? Status { get; set; }
}