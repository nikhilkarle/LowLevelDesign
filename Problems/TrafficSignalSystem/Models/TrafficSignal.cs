using System.Xml;
using TrafficSignalSystem.State;

namespace TrafficSignalSystem.Models;

public sealed class TrafficSignal
{
    public string Id {get;}
    public SignalColor Color => _state.Color;

    private ISignalState _state;

    public TrafficSignal(string id)
    {
        Id = id;
        _state = new RedState();
    }

    public void ForceRed() => _state = new RedState();
    public void ForceYellow() => _state = new YellowState();
    public void ForceGreen() => _state = new GreenState();

    public void ToRed() => _state = _state.ToRed();
    public void ToYellow() => _state = _state.ToYellow();
    public void ToGreen() => _state = _state.ToGreen();

    public override string ToString() => $"{Id}:{Color}";

}