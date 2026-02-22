namespace TrafficSignalSystem.Strategies;

public interface ITimingPlan
{
    int YellowSeconds {get;}
    int GreenSeconds {get;}
}