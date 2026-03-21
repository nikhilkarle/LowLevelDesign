using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.Services;

public sealed class WeekendSurgeRateStrategy : IRateStrategy
{
    public decimal Calculate(RoomType roomType, decimal baseRate, DateTime checkInDate, DateTime checkOutDate)
    {
        decimal total = 0;
        for (var day = checkInDate.Date; day < checkOutDate.Date; day = day.AddDays(1))
        {
            var multiplier = day.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday ? 1.25m : 1m;
            total += baseRate * multiplier;
        }

        return total;
    }
}