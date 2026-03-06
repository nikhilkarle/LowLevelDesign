namespace TicTacToe.Domain;

public sealed record MoveResult(bool Success, string Message)
{
    public static MoveResult Ok(string message = "OK") => new (true, message);
    public static MoveResult Fail(string message) => new (false, message);
}