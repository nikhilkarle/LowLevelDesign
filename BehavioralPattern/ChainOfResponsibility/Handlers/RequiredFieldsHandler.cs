using System;
using COR.Models;

namespace COR.Handlers
{
    public class RequiredFieldsHandler : HandlerBase
    {
        protected override PaymentResult Process(PaymentRequest request)
        {
            if (request == null)
            {
                return PaymentResult.Rejected("Request is null.");
            }

            if (string.IsNullOrWhiteSpace(request.CardNumber))
            {
                return PaymentResult.Rejected("CardNumber is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Currency))
            {
                return PaymentResult.Rejected("Currency is required.");
            }

            if (string.IsNullOrWhiteSpace(request.CustomerId))
            {
                return PaymentResult.Rejected("CustomerId is required.");
            }

            return PaymentResult.Approved("Required fields OK.");
        }
    }
}
