using StrategyPattern.Models;

namespace StrategyPattern.Stratagies
{
    public class PercentDiscountPricingStrategy : IPricingStratagy
    {
        private readonly decimal _percentOff;

        public PercentDiscountPricingStrategy(decimal percentOff)
        {
            if (percentOff < 0 || percentOff > 1)
                throw new ArgumentOutOfRangeException($"Invalid Offer Percent");

            _percentOff = percentOff;
        }

        public decimal CalculateTotal(IEnumerable<CartItem> items)
        {
            decimal preDiscount = items.Sum(i => i.PerItemTotal);
            return preDiscount * (1 - _percentOff);
        }
    }
}