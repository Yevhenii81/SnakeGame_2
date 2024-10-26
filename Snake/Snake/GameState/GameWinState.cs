using static System.Console;

namespace SnakeGame.GameState
{
    public class GameWinState : IGameState
    {
        private readonly int _score;

        // Конструктор принимает текущий счет
        public GameWinState(int score)
        {
            _score = score;
        }

        // Метод для обработки состояния победы
        public void Handle(GameContext context)
        {
            Run(); // Вызов отображения победного экрана
        }

        // Метод для отображения экрана победы
        public void Run()
        {
            Clear(); // Очистить экран
            SetCursorPosition(WindowWidth / 3, WindowHeight / 2); // Установить курсор на центр экрана
            WriteLine($"You Win! Final Score: {_score}"); // Отображение сообщения о победе

            Task.Run(() => Beep(1500, 600)); // Звук победы
            ShowGameOverMenu(); // Показать меню после игры
        }

        // Метод для отображения меню окончания игры
        private void ShowGameOverMenu()
        {
            WriteLine("\nPress any key to return to the main menu...");
            ReadKey(true); // Ожидание нажатия любой клавиши
            var context = new GameContext(new GameStartState()); // Вернуться к начальному состоянию
            context.Request(); // Вызвать начальное состояние
        }
    }
}
