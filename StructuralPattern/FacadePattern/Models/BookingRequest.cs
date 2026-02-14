using System;

namespace FacadePattern.Models
{
    public class BookingRequest
    {
        public string GuestName { get; }
        public DateTime CheckInDate { get; }
        public int Nights { get; }
        public int RoomCount { get; }
        public string Currency { get; }
        public decimal MaxAmountToCharge { get; }

        public BookingRequest(string guestName, DateTime checkInDate, int nights, int roomCount, string currency, decimal maxAmountToCharge)
        {
            GuestName = guestName;
            CheckInDate = checkInDate;
            Nights = nights;
            RoomCount = roomCount;
            Currency = currency;
            MaxAmountToCharge = maxAmountToCharge;
        }
    }
}
