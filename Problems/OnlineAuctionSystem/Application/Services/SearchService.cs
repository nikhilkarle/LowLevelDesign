using OAS.Application.Interfaces;
using OAS.Domain.Entities;
using OAS.Domain.Models;
using OAS.Domain.Specifications;
using OAS.Infrastructure.Repositories;

namespace OAS.Application.Services;
public class SearchService : ISearchService
{
    private readonly IAuctionRepository _auctionRepository;

    public SearchService(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public IEnumerable<Auction> Search(AuctionSearchCriteria criteria)
    {
        IEnumerable<Auction> auctions = _auctionRepository.GetAll();

        var specs = new List<ISpecification<Auction>>();

        if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            specs.Add(new KeywordSpecification(criteria.Keyword));

        if (!string.IsNullOrWhiteSpace(criteria.Category))
            specs.Add(new CategorySpecification(criteria.Category));

        if (criteria.MinPrice.HasValue || criteria.MaxPrice.HasValue)
            specs.Add(new PriceRangeSpecification(criteria.MinPrice, criteria.MaxPrice));

        if (criteria.Status.HasValue)
            specs.Add(new StatusSpecification(criteria.Status.Value));

        return auctions.Where(a => specs.All(spec => spec.IsSatisfiedBy(a)));
    }
}