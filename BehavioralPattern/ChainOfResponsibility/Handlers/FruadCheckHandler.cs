using COR.Models;

namespace COR.Handlers
{
    public class FraudCheckHandler : HandlerBase
    {
        protected override PaymentResult Process(PaymentRequest request)
        {
            if (request.CardNumber.EndsWith("0000"))
            {
                return PaymentResult.Rejected("Fraud rule triggered: suspicious card pattern.");
            }

            if (request.CustomerId == "FLAGGED")
            {
                return PaymentResult.Rejected("Customer is flagged for review.");
            }

            return PaymentResult.Approved("Fraud check OK.");
        }
    }
}
