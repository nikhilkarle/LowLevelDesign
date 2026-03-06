using TicTacToe.Domain;

namespace TicTacToe.Rules;

public sealed class StandardTicTacToeRules : IRules
{
    public GameResult Evaluate(Board board, Move lastMove, int winLength)
    {
        if (winLength < 3)
            throw new ArgumentOutOfRangeException(nameof(winLength), "Win length must be at least 3.");
        if (winLength > board.Size)
            throw new ArgumentOutOfRangeException(nameof(winLength), "Win length cannot exceed board size.");

        var symbol = lastMove.Player.Symbol;

        if (CountInLine(board, lastMove.Row, lastMove.Col, 0, 1, symbol) >= winLength) return GameResult.Win(lastMove.Player);
        if (CountInLine(board, lastMove.Row, lastMove.Col, 1, 0, symbol) >= winLength) return GameResult.Win(lastMove.Player);
        if (CountInLine(board, lastMove.Row, lastMove.Col, 1, 1, symbol) >= winLength) return GameResult.Win(lastMove.Player);
        if (CountInLine(board, lastMove.Row, lastMove.Col, -1, 1, symbol) >= winLength) return GameResult.Win(lastMove.Player);

        if (board.IsFull())
            return GameResult.Draw();

        return GameResult.InProgress();
    }

    private static int CountInLine(Board board, int row, int col, int dr, int dc, char symbol)
    {
        var count = 1;

        count += CountDirection(board, row, col, dr, dc, symbol);

        count += CountDirection(board, row, col, -dr, -dc, symbol);

        return count;
    }
    private static int CountDirection(Board board, int row, int col, int dr, int dc, char symbol)
    {
        var r = row + dr;
        var c = col + dc;
        var count = 0;

        while (r >= 0 && r < board.Size && c >= 0 && c < board.Size)
        {
            var cell = board.GetCell(r, c);
            if (cell != symbol) break;

            count++;
            r += dr;
            c += dc;
        }

        return count;
    }
}