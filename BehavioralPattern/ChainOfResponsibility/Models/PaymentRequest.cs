namespace COR.Models
{
    public class PaymentRequest
    {
        public string CardNumber {get;}
        public string Currency {get;}
        public decimal Amount {get;}
        public string CustomerId {get;}

        public PaymentRequest(string cardNumber, string currency, decimal amount, string customerId)
        {
            CardNumber = cardNumber;
            Currency = currency;
            Amount = amount;
            CustomerId = customerId;
        }
    }
}