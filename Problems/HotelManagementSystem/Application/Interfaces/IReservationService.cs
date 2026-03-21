using HotelManagementSystem.Application.DTOs;
using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Application.Interfaces;

public interface IReservationService
{
    Reservation CreateReservation(CreateReservationRequest request);
    void CancelReservation(Guid reservationId);
    void CheckIn(CheckInRequest request);
    void CheckOut(CheckOutRequest request);
}