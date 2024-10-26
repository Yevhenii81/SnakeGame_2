namespace SnakeGame.GameState
{
    public class GameContext
    {
        public IGameState CurrentState { get; set; } // Текущее состояние игры

        // Конструктор, который задает начальное состояние
        public GameContext(IGameState initialState)
        {
            CurrentState = initialState;
        }

        // Метод для выполнения текущего состояния
        public void Request()
        {
            CurrentState.Handle(this); // Обработка текущего состояния
        }
    }
}
