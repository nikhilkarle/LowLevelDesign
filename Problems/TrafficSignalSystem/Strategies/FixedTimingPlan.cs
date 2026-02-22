namespace TrafficSignalSystem.Strategies;

public class FixedTimingPlan : ITimingPlan
{
    public int YellowSeconds{get;}
    public int GreenSeconds{get;}

    public FixedTimingPlan(int yellowSeconds, int greenSeconds)
    {
        if (greenSeconds <= 0) throw new ArgumentOutOfRangeException(nameof(greenSeconds));
        if (yellowSeconds <= 0) throw new ArgumentOutOfRangeException(nameof(yellowSeconds));
        
        YellowSeconds = yellowSeconds;
        GreenSeconds = greenSeconds;
    }
}