using TrafficSignalSystem.Models;

namespace TrafficSignalSystem.State;

public sealed class GreenState : ISignalState
{
    public SignalColor Color => SignalColor.Green;

    public ISignalState ToGreen() => this;

    public ISignalState ToYellow() => new YellowState();

    public ISignalState ToRed() => this;
    
}