using System;
using AdapterPattern.Adapters;
using AdapterPattern.App;
using AdapterPattern.Vendor;

namespace AdapterPattern
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            VendorCharge vendorCharge = new VendorCharge();
            IPaymentGateway adapter = new VendorPayAdapter(vendorCharge);
            PaymentService service = new PaymentService(adapter);

            PaymentRequest ok = new PaymentRequest("4111111111111111", 120.50m, "USD");
            PaymentResponse okResp = service.ProcessPayment(ok);
            Console.WriteLine("OK => " + okResp.Success + " | " + okResp.TransactionId + " | " + okResp.Message);

            PaymentRequest tooLarge = new PaymentRequest("4111111111111111", 999.00m, "USD");
            PaymentResponse badResp = service.ProcessPayment(tooLarge);
            Console.WriteLine("BIG => " + badResp.Success + " | " + badResp.TransactionId + " | " + badResp.Message);

            
        }
    }
}