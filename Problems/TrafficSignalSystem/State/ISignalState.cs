using TrafficSignalSystem.Models;

namespace TrafficSignalSystem.State;

public interface ISignalState
{
    SignalColor Color {get;}
    ISignalState ToGreen();
    ISignalState ToRed();
    ISignalState ToYellow();
}