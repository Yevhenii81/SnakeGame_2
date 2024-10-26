using SnakeGame.GameState;
using SnakeGame.Helpers;

namespace SnakeGame
{
    public class GameManager
    {
        private IGameState _currentState;

        public GameManager()
        {
            _currentState = new GameStartState();
        }

        public void StartGame()
        {
            ConsoleHelper consoleHelper = new ConsoleHelper();
            consoleHelper.CenterConsoleWindow();

            GamePlayState gamePlayState = new GamePlayState();

            // Подписка на события победы и поражения
            gamePlayState.OnGameWin += HandleGameWin;
            gamePlayState.OnGameLoss += HandleGameLoss;

            _currentState = gamePlayState; // Установка текущего состояния в gamePlayState

            while (true)
            {
                _currentState.Run();
                SwitchState();
            }
        }


        // Метод для обработки победы
        private void HandleGameWin(int score)
        {
            _currentState = new GameWinState(score); // Переключение в состояние победы
        }

        // Метод для обработки поражения
        private void HandleGameLoss(int score)
        {
            _currentState = new GameOverState(score); // Переключение в состояние проигрыша
        }

        private void SwitchState()
        {
            if (_currentState is GameStartState)
                _currentState = new GamePlayState();
            else if (_currentState is GamePlayState)
                _currentState = new GameOverState(GetScore()); // Здесь можно передавать реальный счет
            else if (_currentState is GameOverState)
                _currentState = new GameStartState();
        }

        private int GetScore()
        {
            // Логика получения счета
            return 0; // Замените на реальный счет
        }
    }
}
