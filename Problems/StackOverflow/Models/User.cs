namespace StackOverflow.Models;

public sealed class User
{
    public long Id {get; init;}
    public string Name{get; init;}
    public int ReputationScore {get; private set;}

    public void AddReputation(int delta) => ReputationScore += delta;
}