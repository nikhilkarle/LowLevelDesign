using TrafficSignalSystem.Models;

namespace TrafficSignalSystem.State;

public sealed class YellowState : ISignalState
{
    public SignalColor Color => SignalColor.Yellow;

    public ISignalState ToGreen() => this;

    public ISignalState ToYellow() => this;

    public ISignalState ToRed() => new RedState();
    
}