using StrategyPattern.Stratagies;
using StrategyPattern.Models;

namespace StrategyPattern.Context
{
    public class ContextService
    {
        private IPricingStratagy _setStrategy;

        public ContextService(IPricingStratagy pricingStrategy)
        {
            _setStrategy = pricingStrategy;
        }

        public void SetStrategy(IPricingStratagy pricingStrategy)
        {
            _setStrategy = pricingStrategy;
        }

        public decimal Checkout(IEnumerable<CartItem> items)
        {
            return _setStrategy.CalculateTotal(items);
        }
    }
}