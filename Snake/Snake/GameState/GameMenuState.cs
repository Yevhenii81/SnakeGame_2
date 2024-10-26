using SnakeGame.Menu;
using static System.Console;

namespace SnakeGame.GameState
{
    public class GameMenuState : IGameState
    {
        public void Handle(GameContext context)
        {
            Run();
        }

        public void Run()
        {
            MenuManager.ShowMenu(); // Показать меню через MenuManager
        }
    }
}
