using TicTacToe.Domain;

namespace TicTacToe.Rules;

public interface IRules
{
    GameResult Evaluate(Board board, Move lastMove, int winLength);
}