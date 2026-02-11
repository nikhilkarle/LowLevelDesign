using COR.Models;

namespace COR.Handlers
{
    public abstract class HandlerBase
    {
        private HandlerBase _next;

        public void SetNext(HandlerBase next)
        {
            _next = next;
        }

        public PaymentResult Handle(PaymentRequest paymentRequest)
        {
            PaymentResult result = Process(paymentRequest);

            if (result.IsApproved == false)
            {
                return result;
            }

            if (_next != null)
            {
                return _next.Handle(paymentRequest);
            }

            return result;
        }

        protected abstract PaymentResult Process(PaymentRequest paymentRequest);
    }
}