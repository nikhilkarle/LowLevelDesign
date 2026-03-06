using TicTacToe.Domain;
using TicTacToe.Rules;
using TicTacToe.UI;

namespace TicTacToe;

internal static class Program
{
    private static void Main()
    {
        const int boardSize = 3;
        const int winLength = 3;

        var ui = new ConsoleUI();
        var rules = new StandardTicTacToeRules();
        var game = new Game(boardSize, winLength, rules);

        ui.WriteLine("Tic-Tac-Toe");
        ui.WriteLine("-------------");
        ui.WriteLine($"Board: {boardSize}x{boardSize}, Win length: {winLength}");
        ui.WriteLine("");

        var p1Symbol = ui.PromptForSymbol("Player 1: choose a single character symbol (default X): ", defaultSymbol: 'X');
        var p2Symbol = ui.PromptForSymbol("Player 2: choose a single character symbol (default O): ", defaultSymbol: 'O', disallowed: new[] { p1Symbol });

        var p1 = new Player(Id: 1, Name: "Player 1", Symbol: p1Symbol);
        var p2 = new Player(Id: 2, Name: "Player 2", Symbol: p2Symbol);

        game.Start(p1, p2);

        while (game.State.Status == GameStatus.InProgress)
        {
            ui.Render(game.State);

            var current = game.State.CurrentPlayer!;
            ui.WriteLine($"{current.Name}'s turn ({current.Symbol}).");
            ui.WriteLine("Enter move as: row col   (1-based). Example: 2 3");
            ui.WriteLine("Commands: U = undo last move, Q = quit");
            ui.Write("> ");

            var input = ui.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                ui.WriteLine("Please enter a move or a command.");
                continue;
            }

            input = input.Trim();

            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                ui.WriteLine("Quitting game.");
                return;
            }

            if (input.Equals("U", StringComparison.OrdinalIgnoreCase))
            {
                var undo = game.UndoLastMove();
                if (!undo.Success)
                    ui.WriteLine($"Undo failed: {undo.Message}");
                continue;
            }

            if (!ConsoleUI.TryParseRowCol(input, out var row1Based, out var col1Based))
            {
                ui.WriteLine("Invalid input format. Example: 2 3");
                continue;
            }

            var row = row1Based - 1;
            var col = col1Based - 1;

            var moveResult = game.TryPlayMove(row, col);

            if (!moveResult.Success)
            {
                ui.WriteLine($"Move rejected: {moveResult.Message}");
                continue;
            }

            if (game.State.Status != GameStatus.InProgress)
                break;
        }

        ui.Render(game.State);

        if (game.State.Result is { Kind: GameResultKind.Win } win)
        {
            ui.WriteLine($"{win.Winner!.Name} wins! ({win.Winner.Symbol})");
        }
        else if (game.State.Result is { Kind: GameResultKind.Draw })
        {
            ui.WriteLine("It's a draw!");
        }
        else
        {
            ui.WriteLine("Game ended.");
        }
    }
}