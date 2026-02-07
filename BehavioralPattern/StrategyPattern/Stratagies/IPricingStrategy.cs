using StrategyPattern.Models;

namespace StrategyPattern.Stratagies
{
    public interface IPricingStratagy
    {
        decimal CalculateTotal(IEnumerable<CartItem>items);
    }
}