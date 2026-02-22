using System.Threading.Channels;
using TrafficSignalSystem.Controller;
using TrafficSignalSystem.Events;
using TrafficSignalSystem.Models;
using TrafficSignalSystem.Sensors;
using TrafficSignalSystem.Strategies;

namespace TrafficSignalSystem;

internal static class Program
{
    public static async Task Main()
    {
        var ns = new TrafficSignal("NS");
        var ew = new TrafficSignal("EW");

        var intersection = new Intersection(
            id: "I-1",
            signals: new[] { ns, ew },
            phases: new[]
            {
                new Phase("NS_Green", greenSignalId: "NS", redSignalId: "EW"),
                new Phase("EW_Green", greenSignalId: "EW", redSignalId: "NS"),
            });

        var planSelector = new TimingPlanSelector(
            low: new FixedTimingPlan(greenSeconds: 8,  yellowSeconds: 3),
            medium: new FixedTimingPlan(greenSeconds: 12, yellowSeconds: 3),
            heavy: new FixedTimingPlan(greenSeconds: 18, yellowSeconds: 3),
            emergency: new FixedTimingPlan(greenSeconds: 25, yellowSeconds: 3) 
        );

        var channel = Channel.CreateUnbounded<IIntersectionEvent>(
            new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });

        ISensor vehicleSensor = new RandomVehicleCounter(minIntervalMs: 350, maxIntervalMs: 1100);
        ISensor emergencySensor = new RandomEmergencyDetector(chancePerCheck: 0.04);

        using var cts = new CancellationTokenSource();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        var sensorTasks = new[]
        {
            Task.Run(() => vehicleSensor.RunAsync(channel.Writer, cts.Token)),
            Task.Run(() => emergencySensor.RunAsync(channel.Writer, cts.Token))
        };

        var controller = new IntersectionController(intersection, planSelector, channel.Reader);
        controller.Start(initialPhaseIndex: 0);

        Console.WriteLine("Running... Press Ctrl+C to stop.\n");

        try
        {
            while (!cts.IsCancellationRequested)
            {
                controller.Tick();
                await Task.Delay(1000, cts.Token);
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            channel.Writer.TryComplete();
            await Task.WhenAll(sensorTasks);
        }
    }
}
