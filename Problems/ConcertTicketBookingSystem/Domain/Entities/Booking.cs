using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.Entities;

public class Booking
{
    public Guid                  Id             { get; }
    public Guid                  UserId         { get; }
    public Guid                  ConcertId      { get; }
    public IReadOnlyList<Guid>   ConcertSeatIds { get; }
    public BookingStatus         Status         { get; private set; }
    public decimal               TotalAmount    { get; }
    public DateTime              CreatedAt      { get; }

    public Booking(Guid id, Guid userId, Guid concertId,
                   IReadOnlyList<Guid> seatIds, decimal totalAmount)
    {
        Id = id; UserId = userId; ConcertId = concertId;
        ConcertSeatIds = seatIds; TotalAmount = totalAmount;
        Status = BookingStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetStatus(BookingStatus status) => Status = status;
}
