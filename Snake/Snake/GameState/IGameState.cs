namespace SnakeGame.GameState
{
    public interface IGameState
    {
        void Run();

        void Handle(GameContext context); // Метод для обработки текущего состояния
    }
}
