using System.Threading.Channels;
using TrafficSignalSystem.Events;

namespace TrafficSignalSystem.Sensors;

public sealed class RandomVehicleCounter : ISensor
{
    private readonly Random _rng = new();
    private readonly int _minMs;
    private readonly int _maxMs;

    public RandomVehicleCounter(int minIntervalMs = 400, int maxIntervalMs = 1200)
    {
        if (minIntervalMs <= 0) throw new ArgumentOutOfRangeException(nameof(minIntervalMs));
        if (maxIntervalMs < minIntervalMs) throw new ArgumentOutOfRangeException(nameof(maxIntervalMs));

        _minMs = minIntervalMs;
        _maxMs = maxIntervalMs;
    }

    public async Task RunAsync(ChannelWriter<IIntersectionEvent> writer, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(_rng.Next(_minMs, _maxMs + 1), ct);

            int count = _rng.Next(0, 31);
            await writer.WriteAsync(new VehicleCountEvent(count, DateTimeOffset.UtcNow), ct);
        }
    }
}