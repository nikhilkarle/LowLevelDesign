using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.Interfaces;

public interface IRateStrategy
{
    decimal Calculate(RoomType roomType, decimal baseRate, DateTime checkInDate, DateTime checkOutDate);
}