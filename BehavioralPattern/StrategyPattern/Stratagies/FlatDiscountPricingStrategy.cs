using StrategyPattern.Models;

namespace StrategyPattern.Stratagies
{
    public class FlatDiscountPricingStrategy : IPricingStratagy
    {
        private readonly decimal _flatOff;

        public FlatDiscountPricingStrategy(decimal flatOff)
        {
            if (flatOff < 0)
                throw new ArgumentOutOfRangeException($"Invalid Offer Percent");

            _flatOff = flatOff;
        }

        public decimal CalculateTotal(IEnumerable<CartItem> items)
        {
            decimal preDiscount = items.Sum(i => i.PerItemTotal);
            decimal total = preDiscount - _flatOff;
            return total < 0 ? 0 : total;
        }
    }
}