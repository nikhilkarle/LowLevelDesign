using FacadePattern.Models;

namespace FacadePattern.Subsystems
{
    public class PaymentService
    {
        public bool Charge(string guestName, decimal amount, string currency, decimal maxAmountToCharge)
        {
            if (amount <= 0)
            {
                return false;
            }

            if (amount > maxAmountToCharge)
            {
                return false;
            }

            return true;
        }
    }
}
