namespace HotelManagementSystem.Application.Interfaces;

public interface IReportService
{
    decimal GetOccupancyRate(DateTime date);
    decimal GetRevenue(DateTime from, DateTime to);
}