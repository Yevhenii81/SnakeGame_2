using SnakeGame.GameState;

namespace SnakeGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IGameState game = new GamePlayState(); // Создание экземпляра класса GamePlayState
            game.Run(); // Запуск игры
        }
    }
}