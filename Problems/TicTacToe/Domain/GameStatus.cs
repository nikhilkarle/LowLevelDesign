namespace TicTacToe.Domain;

public enum GameStatus
{
    NotStarted,
    InProgress,
    Finished
}

public sealed record GameState(
    GameStatus Status,
    Board Board,
    Player? CurrentPlayer,
    Player? Player1,
    Player? Player2,
    GameResult? Result
);