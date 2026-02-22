using System.Threading.Channels;
using TrafficSignalSystem.Events;

namespace TrafficSignalSystem.Sensors;

public sealed class RandomEmergencyDetector : ISensor
{
    private readonly Random _rng = new();
    private readonly double _chancePerCheck;
    private bool _active;

    public RandomEmergencyDetector(double chancePerCheck = 0.03)
    {
        if (chancePerCheck < 0 || chancePerCheck > 1)
            throw new ArgumentOutOfRangeException(nameof(chancePerCheck));

        _chancePerCheck = chancePerCheck;
    }

    public async Task RunAsync(ChannelWriter<IIntersectionEvent> writer, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(700, ct);

            if (!_active && _rng.NextDouble() < _chancePerCheck)
            {
                _active = true;
                await writer.WriteAsync(new EmergencyDetectedEvent(DateTimeOffset.UtcNow), ct);
                continue;
            }

            if (_active && _rng.NextDouble() < 0.20)
            {
                _active = false;
                await writer.WriteAsync(new EmergencyClearedEvent(DateTimeOffset.UtcNow), ct);
            }
        }
    }
}
