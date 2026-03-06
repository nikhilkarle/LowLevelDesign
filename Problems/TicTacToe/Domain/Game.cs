using TicTacToe.Rules;

namespace TicTacToe.Domain;

public sealed class Game
{
    private readonly IRules _rules;
    private readonly int _winLength;
    private readonly Stack<Move> _history = new();

    public GameState State { get; private set; }

    public Game(int boardSize, int winLength, IRules rules)
    {
        _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        _winLength = winLength;

        var board = new Board(boardSize);
        State = new GameState(
            Status: GameStatus.NotStarted,
            Board: board,
            CurrentPlayer: null,
            Player1: null,
            Player2: null,
            Result: null
        );
    }

    public void Start(Player player1, Player player2)
    {
        if (player1.Symbol == player2.Symbol)
            throw new ArgumentException("Players must have different symbols.");

        var newBoard = new Board(State.Board.Size);
        _history.Clear();

        State = State with
        {
            Status = GameStatus.InProgress,
            Board = newBoard,
            Player1 = player1,
            Player2 = player2,
            CurrentPlayer = player1,
            Result = GameResult.InProgress()
        };
    }

    public MoveResult TryPlayMove(int row, int col)
    {
        if (State.Status != GameStatus.InProgress)
            return MoveResult.Fail("Game is not in progress.");

        var current = State.CurrentPlayer;
        if (current is null)
            return MoveResult.Fail("No current player set.");

        if (row < 0 || row >= State.Board.Size || col < 0 || col >= State.Board.Size)
            return MoveResult.Fail("Move out of bounds.");

        if (!State.Board.IsEmpty(row, col))
            return MoveResult.Fail("Cell is already occupied.");

        State.Board.Place(row, col, current.Symbol);

        var move = new Move(row, col, current);
        _history.Push(move);

        var result = _rules.Evaluate(State.Board, move, _winLength);

        if (result.Kind == GameResultKind.Win || result.Kind == GameResultKind.Draw)
        {
            State = State with
            {
                Status = GameStatus.Finished,
                Result = result,
                CurrentPlayer = null
            };
            return MoveResult.Ok("Game finished.");
        }

        var next = GetOtherPlayer(current);
        State = State with
        {
            Result = result,
            CurrentPlayer = next
        };

        return MoveResult.Ok("Move accepted.");
    }

    public MoveResult UndoLastMove()
    {
        if (State.Status == GameStatus.NotStarted)
            return MoveResult.Fail("Game has not started.");

        if (_history.Count == 0)
            return MoveResult.Fail("No moves to undo.");

        var last = _history.Pop();
        State.Board.Clear(last.Row, last.Col);

        var p1 = State.Player1!;
        var p2 = State.Player2!;
        var current = last.Player.Id == p1.Id ? p1 : p2;

        State = State with
        {
            Status = GameStatus.InProgress,
            CurrentPlayer = current,
            Result = GameResult.InProgress()
        };

        return MoveResult.Ok("Undid last move.");
    }

    private Player GetOtherPlayer(Player current)
    {
        if (State.Player1 is null || State.Player2 is null)
            throw new InvalidOperationException("Players not initialized.");

        return current.Id == State.Player1.Id ? State.Player2 : State.Player1;
    }
}