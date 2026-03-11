using ElevatorSystem.Interfaces;
using ElevatorSystem.Requests;

namespace ElevatorSystem.Controllers;

public class ElevatorDispatcher
{
    private readonly object _lock = new();
    private readonly IReadOnlyList<ElevatorController> _elevators;
    private readonly IElevatorDispatchStrategy _dispatchStrategy;

    public ElevatorDispatcher(
        IReadOnlyList<ElevatorController> elevators,
        IElevatorDispatchStrategy dispatchStrategy)
    {
        _elevators = elevators ?? throw new ArgumentNullException(nameof(elevators));
        _dispatchStrategy = dispatchStrategy ?? throw new ArgumentNullException(nameof(dispatchStrategy));
    }

    public void SubmitHallRequest(HallRequest request)
    {
        lock (_lock)
        {
            var elevator = _dispatchStrategy.SelectBestElevator(_elevators, request);
            elevator.AssignHallRequest(request);
        }
    }
}