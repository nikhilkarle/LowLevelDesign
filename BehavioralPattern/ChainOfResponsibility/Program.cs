using COR.Handlers;
using COR.Models;

namespace COR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RequiredFieldsHandler reqCheck = new RequiredFieldsHandler();
            AmountLimitHandler limitCheck = new AmountLimitHandler(500m);
            FraudCheckHandler fraudCheck = new FraudCheckHandler();
            ApproveHandler approve = new ApproveHandler();

            reqCheck.SetNext(limitCheck);
            limitCheck.SetNext(fraudCheck);
            fraudCheck.SetNext(approve);

            PaymentRequest ok = new PaymentRequest("4111111111111234", "USD", 120m, "CUST-1");
            PaymentRequest tooBig = new PaymentRequest("4111111111111234", "USD", 999m, "CUST-1");
            PaymentRequest fraudCard = new PaymentRequest("4111111111110000", "USD", 50m, "CUST-1");

            Run(reqCheck, ok);
            Run(reqCheck, tooBig);
            Run(reqCheck, fraudCard);
        }

        private static void Run(HandlerBase chainStart, PaymentRequest request)
        {
            PaymentResult result = chainStart.Handle(request);
            Console.WriteLine("Approved: " + result.IsApproved + " | " + result.Message);
        }
    }
}