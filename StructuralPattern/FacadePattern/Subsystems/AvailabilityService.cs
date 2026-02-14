using System;
using FacadePattern.Models;

namespace FacadePattern.Subsystems
{
    public class AvailabilityService
    {
        public bool HasAvailability(BookingRequest request)
        {
            if (request.RoomCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
