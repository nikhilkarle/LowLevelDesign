namespace AdapterPattern.App
{
    public class PaymentRequest
    {
        public string CardNumber { get; }
        public decimal Amount { get; }
        public string Currency { get; }

        public PaymentRequest(string cardNumber, decimal amount, string currency)
        {
            CardNumber = cardNumber;
            Amount = amount;
            Currency = currency;
        }
    }
}