namespace TrafficSignalSystem.Models;

public sealed class Intersection
{
    public string Id {get;}
    public IReadOnlyList<TrafficSignal> Signals {get;}
    public IReadOnlyList<Phase> Phases{get;}

    public Intersection(string id, IEnumerable<TrafficSignal> signals, IEnumerable<Phase> phases)
    {
        Id = id;
        Signals = signals.ToList();
        Phases = phases.ToList();

        if (Signals.Count == 0) throw new ArgumentException("Intersection must have at least one signal.");
        if (Phases.Count == 0) throw new ArgumentException("Intersection must have at least one phase.");

        if (Signals.Select(s => s.Id).Distinct().Count() != Signals.Count) throw new ArgumentException("Duplicate Signal IDs found.");

        var ids = Signals.Select(s => s.Id).ToHashSet();
        foreach (var p in Phases)
        {
            if (!ids.Contains(p.greenSignalId) || !ids.Contains(p.redSignalId))
                throw new ArgumentException($"Phase '{p.Name}' refers to unknown signal IDs.");
        }
    }

    public TrafficSignal GetSignal(string id)
    {
        TrafficSignal signal = Signals.First(s => s.Id == id);
        return signal;
    }
}