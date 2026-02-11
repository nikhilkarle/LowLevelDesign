using COR.Models;

namespace COR.Handlers
{
    public class ApproveHandler : HandlerBase
    {
        protected override PaymentResult Process(PaymentRequest request)
        {
            return PaymentResult.Approved("Payment approved.");
        }
    }
}
