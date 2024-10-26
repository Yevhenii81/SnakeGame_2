using static System.Console;

namespace SnakeGame.GameState
{
    public class GameStartState : IGameState
    {
        public void Handle(GameContext context)
        {
            Run();
            context.CurrentState = new GamePlayState();
        }

        public void Run()
        {
            ShowLogo();
        }

        public static void ShowLogo()
        {
            Clear();

            // Логотип в виде массива строк
            var logoLines = new[]
            {
                "  SSSSS  N   N   AAAAA   K   K  EEEEE ",
                " S       NN  N   A   A   K  K   E     ",
                "  SSS    N N N   AAAAA   KKK    EEEEE ",
                "     S   N  NN   A   A   K  K   E     ",
                " SSSSS   N   N   A   A   K   K  EEEEE "
            };

            int maxLength = logoLines[0].Length;

            // Центрирование
            int startX = (WindowWidth - maxLength) / 2;
            int startY = (WindowHeight - logoLines.Length) / 2;

            // Рисуем логотип по буквам вертикально
            for (int col = 0; col < maxLength; col++)
            {
                for (int row = 0; row < logoLines.Length; row++)
                {
                    if (col < logoLines[row].Length) // Проверка, чтобы не выйти за пределы строки
                    {
                        SetCursorPosition(startX + col, startY + row);
                        Write(logoLines[row][col]);
                    }
                }
                System.Threading.Thread.Sleep(100); // Задержка между столбцами
            }

            // Показ сообщения
            SetCursorPosition(startX, startY + logoLines.Length + 1);
            Write("Press Enter to start...");

            // Ожидание нажатия клавиши Enter
            while (true)
            {
                if (KeyAvailable && ReadKey(true).Key == ConsoleKey.Enter)
                    break;
            }
        }
    }
}
