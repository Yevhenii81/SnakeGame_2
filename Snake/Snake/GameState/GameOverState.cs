namespace SnakeGame.GameState;
using static System.Console;
using SnakeGame.Menu;

public class GameOverState : IGameState
{
    private readonly int _finalScore;

    public GameOverState(int finalScore)
    {
        _finalScore = finalScore;
    }

    public void Handle(GameContext context)
    {
        Run();
        context.CurrentState = new GameMenuState();
    }

    public void Run()
    {
        LoseGame(_finalScore); // Вызов метода проигрыша
    }

    private void LoseGame(int score)
    {
        SetCursorPosition(WindowWidth / 3, WindowHeight / 2);
        WriteLine($"Game over, score: {score}");
        Task.Run(() => Beep(200, 600)); // Звук проигрыша
        ShowGameOverMenu(); // Показать меню окончания игры
    }

    //метод для отображения меню окончания игры
    static void ShowGameOverMenu()
    {
        WriteLine("Press any key to return to the menu...");
        ReadKey(); //ожидание нажатия клавиши
        MenuManager.ShowMenu(); //возврат в меню

    }
}