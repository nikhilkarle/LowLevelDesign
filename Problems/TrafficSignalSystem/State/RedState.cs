using TrafficSignalSystem.Models;

namespace TrafficSignalSystem.State;

public sealed class RedState : ISignalState
{
    public SignalColor Color => SignalColor.Red;

    public ISignalState ToGreen() => new GreenState();

    public ISignalState ToYellow() => this;

    public ISignalState ToRed() => this;
    
}