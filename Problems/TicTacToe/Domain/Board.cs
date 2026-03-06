namespace TicTacToe.Domain;

public sealed class Board
{
    private readonly char?[,] _cells;
    public int Size{get;}
    public Board(int size)
    {
        if (size < 3)
            throw new ArgumentOutOfRangeException(nameof(size), "Board size must be at least 3.");

        Size = size;
        _cells = new char?[size, size];
    }
    public char? GetCell(int row, int col)
    {
        ValidateInBounds(row, col);
        return _cells[row, col];
    }

    public bool IsEmpty(int row, int col)
    {
        ValidateInBounds(row, col);
        return _cells[row, col] is null;
    }

    public bool IsFull()
    {
        for (var r = 0; r < Size; r++)
        for (var c = 0; c < Size; c++)
            if (_cells[r, c] is null)
                return false;

        return true;
    }

    public void Place(int row, int col, char symbol)
    {
        ValidateInBounds(row, col);

        if (_cells[row, col] is not null)
            throw new InvalidOperationException("Cell is already occupied.");

        _cells[row, col] = symbol;
    }

    public void Clear(int row, int col)
    {
        ValidateInBounds(row, col);
        _cells[row, col] = null;
    }
    private void ValidateInBounds(int row, int col)
    {
        if (row < 0 || row >= Size || col < 0 || col >= Size)
            throw new ArgumentOutOfRangeException($"Move ({row},{col}) is out of bounds for {Size}x{Size} board.");
    }
}