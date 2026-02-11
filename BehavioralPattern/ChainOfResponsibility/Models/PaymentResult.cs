namespace COR.Models
{
    public class PaymentResult
    {
        public bool IsApproved{get;}
        public string Message{get;}

        public PaymentResult(bool isApproved, string message)
        {
            IsApproved = isApproved;
            Message = message;
        }

        public static PaymentResult Approved(string message)
        {
            return new PaymentResult(true, message);
        }

        public static PaymentResult Rejected(string message)
        {
            return new PaymentResult(false, message);
        }
    }
}