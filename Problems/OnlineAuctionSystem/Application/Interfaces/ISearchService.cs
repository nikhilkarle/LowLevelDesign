using OAS.Domain.Entities;
using OAS.Domain.Models;

namespace OAS.Application.Interfaces;

public interface ISearchService
{
    IEnumerable<Auction> Search(AuctionSearchCriteria criteria);
}