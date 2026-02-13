namespace AdapterPattern.App
{
    public interface IPaymentGateway
    {
        PaymentResponse Charge(PaymentRequest paymentRequest);
    }
}