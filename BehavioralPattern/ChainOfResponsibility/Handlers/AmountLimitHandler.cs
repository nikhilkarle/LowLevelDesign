using COR.Models;

namespace COR.Handlers
{
    public class AmountLimitHandler : HandlerBase
    {
        private readonly decimal _maxAmount;

        public AmountLimitHandler(decimal maxAmount)
        {
            _maxAmount = maxAmount;
        }

        protected override PaymentResult Process(PaymentRequest request)
        {
            if (request.Amount <= 0)
            {
                return PaymentResult.Rejected("Amount must be > 0.");
            }

            if (request.Amount > _maxAmount)
            {
                return PaymentResult.Rejected("Amount exceeds limit of " + _maxAmount + ".");
            }

            return PaymentResult.Approved("Amount OK.");
        }
    }
}
