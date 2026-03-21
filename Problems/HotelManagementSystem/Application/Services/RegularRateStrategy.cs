using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.Services;

public sealed class RegularRateStrategy : IRateStrategy
{
    public decimal Calculate(RoomType roomType, decimal baseRate, DateTime checkInDate, DateTime checkOutDate)
    {
        var nights = (checkOutDate.Date - checkInDate.Date).Days;
        return baseRate * nights;
    }
}