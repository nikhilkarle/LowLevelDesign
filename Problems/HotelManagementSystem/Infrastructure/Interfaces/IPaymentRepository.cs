using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Infrastructure.Interfaces;

public interface IPaymentRepository
{
    void Add(Payment payment);
    IReadOnlyList<Payment> GetByReservationId(Guid reservationId);
}