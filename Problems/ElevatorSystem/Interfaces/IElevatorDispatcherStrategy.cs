using ElevatorSystem.Controllers;
using ElevatorSystem.Requests;

namespace ElevatorSystem.Interfaces;

public interface IElevatorDispatchStrategy
{
    ElevatorController SelectBestElevator(
        IReadOnlyList<ElevatorController> elevators,
        HallRequest request);
}