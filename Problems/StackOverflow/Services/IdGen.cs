namespace StackOverflow.Services;

public static class IdGen
{
    private static long _id = 1000;
    public static long NewId() => Interlocked.Increment(ref _id);
}
