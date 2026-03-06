using TicTacToe.Domain;

namespace TicTacToe.UI;
public sealed class ConsoleUI : IGameUI
{
    public void Render(GameState state)
    {
        Console.WriteLine();
        RenderBoard(state.Board);
        Console.WriteLine();

        if (state.Result is { Kind: GameResultKind.InProgress })
        {
        }
        else if (state.Result is { Kind: GameResultKind.Win } win)
        {
            Console.WriteLine($"Result: {win.Winner!.Name} wins!");
        }
        else if (state.Result is { Kind: GameResultKind.Draw })
        {
            Console.WriteLine("Result: Draw!");
        }
    }

    public void WriteLine(string message) => Console.WriteLine(message);
    public void Write(string message) => Console.Write(message);
    public string? ReadLine() => Console.ReadLine();

    public char PromptForSymbol(string prompt, char defaultSymbol, IEnumerable<char>? disallowed = null)
    {
        var banned = new HashSet<char>(disallowed ?? Array.Empty<char>());

        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return defaultSymbol;

            input = input.Trim();

            if (input.Length != 1)
            {
                Console.WriteLine("Please enter exactly ONE character.");
                continue;
            }

            var ch = input[0];

            if (char.IsWhiteSpace(ch))
            {
                Console.WriteLine("Whitespace is not allowed as a symbol.");
                continue;
            }

            if (banned.Contains(ch))
            {
                Console.WriteLine("That symbol is already taken. Choose a different one.");
                continue;
            }

            return ch;
        }
    }

    public static bool TryParseRowCol(string input, out int row, out int col)
    {
        row = 0;
        col = 0;

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length != 2) return false;

        if (!int.TryParse(parts[0], out row)) return false;
        if (!int.TryParse(parts[1], out col)) return false;

        return true;
    }

    private static void RenderBoard(Board board)
    {
        var size = board.Size;

        Console.Write("    ");
        for (var c = 1; c <= size; c++)
        {
            Console.Write($"{c}   ");
        }
        Console.WriteLine();

        for (var r = 0; r < size; r++)
        {
            Console.Write($"{r + 1}   ");

            for (var c = 0; c < size; c++)
            {
                var cell = board.GetCell(r, c);
                var ch = cell ?? ' ';
                Console.Write($" {ch} ");

                if (c < size - 1) Console.Write("|");
            }

            Console.WriteLine();

            if (r < size - 1)
            {
                Console.Write("    ");
                for (var c = 0; c < size; c++)
                {
                    Console.Write("---");
                    if (c < size - 1) Console.Write("+");
                }
                Console.WriteLine();
            }
        }
    }
}