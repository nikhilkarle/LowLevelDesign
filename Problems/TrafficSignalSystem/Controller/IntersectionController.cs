using TrafficSignalSystem.Events;
using TrafficSignalSystem.Models;
using TrafficSignalSystem.Strategies;
using System.Threading.Channels;
using System.Runtime.CompilerServices;

namespace TrafficSignalSystem.Controller;

public sealed class IntersectionController
{
    private readonly Intersection _intersection;
    private readonly TimingPlanSelector _planSelector;
    private readonly ChannelReader<IIntersectionEvent> _reader;

    private bool _emergencyActive;
    private int _currentPhaseIndex;
    private int _phaseSecond;     
    private int _lastVehicleCount; 

    public IntersectionController(
        Intersection intersection,
        TimingPlanSelector planSelector,
        ChannelReader<IIntersectionEvent> reader)
    {
        _intersection = intersection;
        _planSelector = planSelector;
        _reader = reader;
    }

    public void Start(int initialPhaseIndex)
    {
        _currentPhaseIndex = ((initialPhaseIndex % _intersection.Phases.Count) + _intersection.Phases.Count)
                             % _intersection.Phases.Count;
        _phaseSecond = 0;

        ApplyPhase(_intersection.Phases[_currentPhaseIndex], forceGreenNow: true);
        PrintStatus("START");
    }

    public void Tick()
    {
        DrainIncomingEvents();

        var level = ClassifyTraffic(_lastVehicleCount);
        var plan = _planSelector.Select(level, _emergencyActive);

        _phaseSecond++;

        var phase = _intersection.Phases[_currentPhaseIndex];
        var greenSignal = _intersection.GetSignal(phase.greenSignalId);

        if (greenSignal.Color == SignalColor.Green)
        {
            if (_phaseSecond >= plan.GreenSeconds)
            {
                greenSignal.ToYellow();
                PrintStatus($"GREEN->YELLOW (lvl={level}, emerg={_emergencyActive}, veh={_lastVehicleCount})");
                _phaseSecond = 0;
            }
        }
        else if (greenSignal.Color == SignalColor.Yellow)
        {
            if (_phaseSecond >= plan.YellowSeconds)
            {
                greenSignal.ToRed();
                AdvancePhase();
                PrintStatus($"YELLOW->RED + NEXT PHASE (lvl={level}, emerg={_emergencyActive}, veh={_lastVehicleCount})");
                _phaseSecond = 0;
            }
        }
        else
        {
            ApplyPhase(phase, forceGreenNow: true);
            PrintStatus("SELF-HEAL APPLY PHASE");
            _phaseSecond = 0;
        }
    }

    private void DrainIncomingEvents()
    {
        while (_reader.TryRead(out var ev))
        {
            switch (ev)
            {
                case VehicleCountEvent vc:
                    _lastVehicleCount = vc.Count;
                    break;

                case EmergencyDetectedEvent:
                    _emergencyActive = true;
                    break;

                case EmergencyClearedEvent:
                    _emergencyActive = false;
                    break;
            }
        }
    }

    private void AdvancePhase()
    {
        _currentPhaseIndex = (_currentPhaseIndex + 1) % _intersection.Phases.Count;
        ApplyPhase(_intersection.Phases[_currentPhaseIndex], forceGreenNow: true);
    }

    private void ApplyPhase(Phase phase, bool forceGreenNow)
    {
        var green = _intersection.GetSignal(phase.greenSignalId);
        var red = _intersection.GetSignal(phase.redSignalId);

        red.ForceRed();

        if (forceGreenNow)
            green.ForceGreen();
    }

    private static TrafficLevel ClassifyTraffic(int vehicleCount)
    {
        if (vehicleCount >= 20) return TrafficLevel.Heavy;
        if (vehicleCount >= 10) return TrafficLevel.Medium;
        return TrafficLevel.Low;
    }

    private void PrintStatus(string reason)
    {
        var phase = _intersection.Phases[_currentPhaseIndex];
        var snapshot = string.Join(" | ", _intersection.Signals.Select(s => s.ToString()));
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {reason} | Phase={phase.Name} | {snapshot}");
    }
}
