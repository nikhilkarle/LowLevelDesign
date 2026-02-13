namespace AdapterPattern.App
{
    public class PaymentResponse
    {
        public bool Success { get; }
        public string TransactionId { get; }
        public string Message { get; }

        public PaymentResponse(bool success, string transactionId, string message)
        {
            Success = success;
            TransactionId = transactionId;
            Message = message;
        }
    }
}
