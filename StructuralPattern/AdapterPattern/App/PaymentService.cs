namespace AdapterPattern.App
{
    public class PaymentService
    {
        private readonly IPaymentGateway _gateway;

        public PaymentService(IPaymentGateway gateway)
        {
            _gateway = gateway;
        }

        public PaymentResponse ProcessPayment(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return new PaymentResponse(false, "", "Request is null.");
            }

            if (paymentRequest.Amount <= 0)
            {
                return new PaymentResponse(false, "", "Amount must be > 0.");
            }

            return _gateway.Charge(paymentRequest);
        }
    }
}