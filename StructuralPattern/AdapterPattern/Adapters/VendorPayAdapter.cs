using AdapterPattern.App;
using AdapterPattern.Vendor;

namespace AdapterPattern.Adapters
{
    public class VendorPayAdapter : IPaymentGateway
    {
        private readonly VendorCharge _vendorClient;

        public VendorPayAdapter(VendorCharge vendorClient)
        {
            _vendorClient = vendorClient;
        }

        public PaymentResponse Charge(PaymentRequest request)
        {
            VendorChargeRequest vendorRequest = new VendorChargeRequest();
            vendorRequest.Pan = request.CardNumber;
            vendorRequest.IsoCurrency = request.Currency;

            decimal centsDecimal = request.Amount * 100m;
            int cents = (int)Math.Round(centsDecimal, 0, MidpointRounding.AwayFromZero);
            vendorRequest.AmountInCents = cents;

            VendorChargeResult vendorResult = _vendorClient.MakeCharge(vendorRequest);

            if (vendorResult.Approved)
            {
                return new PaymentResponse(true, vendorResult.RefCode, "Approved");
            }

            return new PaymentResponse(false, vendorResult.RefCode, vendorResult.DeclineReason);
        }
    }
}