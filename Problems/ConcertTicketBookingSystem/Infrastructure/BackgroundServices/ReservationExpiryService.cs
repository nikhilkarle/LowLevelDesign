using ConcertTicketBookingSystem.Application.Interfaces;

namespace ConcertTicketBookingSystem.Infrastructure.BackgroundServices;

public class ReservationExpiryService
{
    private readonly IConcertSeatRepository _seatRepo;
    private readonly TimeSpan               _interval;
    private readonly CancellationTokenSource _cts = new();
    private Task? _task;

    public ReservationExpiryService(IConcertSeatRepository seatRepo, TimeSpan? interval = null)
    {
        _seatRepo = seatRepo;
        _interval = interval ?? TimeSpan.FromMinutes(1);
    }

    public void Start()
    {
        _task = Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                await Task.Delay(_interval, _cts.Token).ConfigureAwait(false);
                _seatRepo.ReleaseExpired();
            }
        }, _cts.Token);
    }

    public void Stop() => _cts.Cancel();
}
