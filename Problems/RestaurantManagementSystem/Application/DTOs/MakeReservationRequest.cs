namespace RestaurantManagementSystem.Application.DTOs;

public class MakeReservationRequest
{
    public Guid CustomerId { get; init; }
    public int PartySize { get; init; }
    public DateTime Date { get; init; }
    public TimeSpan TimeSlot { get; init; }
}
