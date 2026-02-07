using StrategyPattern.Models;

namespace StrategyPattern.Stratagies
{
    public class RegularPricingStrategy : IPricingStratagy
    {
        public decimal CalculateTotal(IEnumerable<CartItem> items) => items.Sum(i => i.PerItemTotal);
    }
}