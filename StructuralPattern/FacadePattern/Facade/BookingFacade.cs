using FacadePattern.Models;
using FacadePattern.Subsystems;

namespace FacadePattern.Facade
{
    public class BookingFacade
    {
        private readonly AvailabilityService _availability;
        private readonly PricingService _pricing;
        private readonly PaymentService _payment;
        private readonly ConfirmationService _confirmation;

        public BookingFacade()
        {
            _availability = new AvailabilityService();
            _pricing = new PricingService();
            _payment = new PaymentService();
            _confirmation = new ConfirmationService();
        }

        public BookingResponse BookRoom(BookingRequest request)
        {
            if (request == null)
            {
                return BookingResponse.Failed("Request is null.");
            }

            bool available = _availability.HasAvailability(request);
            if (!available)
            {
                return BookingResponse.Failed("No availability.");
            }

            decimal total = _pricing.CalculateTotal(request);

            bool charged = _payment.Charge(request.GuestName, total, request.Currency, request.MaxAmountToCharge);
            if (!charged)
            {
                return BookingResponse.Failed("Payment failed.");
            }

            string confirmationNumber = _confirmation.SendConfirmation(request.GuestName);
            return BookingResponse.Ok(confirmationNumber);
        }
    }
}
