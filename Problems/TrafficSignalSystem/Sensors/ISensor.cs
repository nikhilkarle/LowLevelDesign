using System.Threading.Channels;
using TrafficSignalSystem.Events;

namespace TrafficSignalSystem.Sensors;

public interface ISensor
{
    Task RunAsync(ChannelWriter<IIntersectionEvent> writer, CancellationToken ct);
}