using FacadePattern.Models;

namespace FacadePattern.Subsystems
{
    public class PricingService
    {
        public decimal CalculateTotal(BookingRequest request)
        {
            decimal baseNightlyRate = 120m;
            decimal total = baseNightlyRate * request.Nights * request.RoomCount;
            return total;
        }
    }
}
