using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly List<Payment> _payments = new();

    public void Add(Payment payment) => _payments.Add(payment);

    public IReadOnlyList<Payment> GetByReservationId(Guid reservationId) =>
        _payments.Where(x => x.ReservationId == reservationId).ToList();
}