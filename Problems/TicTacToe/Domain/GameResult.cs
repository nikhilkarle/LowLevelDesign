namespace TicTacToe.Domain;

public enum GameResultKind
{
    InProgress,
    Win,
    Draw
}

public sealed record GameResult(GameResultKind Kind, Player? Winner)
{
    public static GameResult InProgress() => new(GameResultKind.InProgress, null);
    public static GameResult Win(Player winner) => new(GameResultKind.Win, winner);
    public static GameResult Draw() => new(GameResultKind.Draw, null);
}