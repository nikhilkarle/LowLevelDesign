namespace TrafficSignalSystem.Strategies;

public sealed class TimingPlanSelector
{
    private readonly ITimingPlan _low;
    private readonly ITimingPlan _medium;
    private readonly ITimingPlan _heavy;
    private readonly ITimingPlan _emergency;

    public TimingPlanSelector(ITimingPlan low, ITimingPlan medium, ITimingPlan heavy, ITimingPlan emergency)
    {
        _low = low;
        _medium = medium;
        _heavy = heavy;
        _emergency = emergency;
    }

    public ITimingPlan Select(TrafficLevel level, bool emergencyActive)
        => emergencyActive ? _emergency
        : level switch
        {
            TrafficLevel.Low => _low,
            TrafficLevel.Medium => _medium,
            TrafficLevel.Heavy => _heavy,
            _ => _medium
        };
}