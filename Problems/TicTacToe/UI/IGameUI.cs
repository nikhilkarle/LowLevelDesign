using TicTacToe.Domain;

namespace TicTacToe.UI;

public interface IGameUI
{
    void Render(GameState state);
    void WriteLine(string message);
    void Write(string message);
    string? ReadLine();
    char PromptForSymbol(string prompt, char defaultSymbol, IEnumerable<char>? disallowed = null);
}