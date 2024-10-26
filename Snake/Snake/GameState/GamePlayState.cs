using SnakeGame.Models;
using SnakeGame.Helpers;
using SnakeGame.Menu;
using SnakeGame.Utilities; // это можно оставить, если вы используете что-то из этого пространства имен
using static System.Console;

namespace SnakeGame.GameState
{
    public class GamePlayState : IGameState
    {
        //делегат для победы и поражения
        public delegate void GameEventHandler(int score);

        public event GameEventHandler? OnGameWin;
        public event GameEventHandler? OnGameLoss;

        public void Run()
        {
            Title = "Snake Game";

            CursorVisible = false; //скрытие курсора

            var consoleHelper = new ConsoleHelper();
            consoleHelper.CenterConsoleWindow();

            while (true)
            {
                GameStartState.ShowLogo(); //показ логотипа игры
                MenuManager.ShowMenu(); //показ главного меню
                ReadKey(); //ожидания нажатия клавиши
            }

            //StartGame(40, 20, true);
        }

        public void StartGame(int mapWidth, int mapHeight, bool withBomb)
        {
            //установка размеров окна и буфера консоли
            SetWindowSize(mapWidth + 1, mapHeight + 1);
            SetBufferSize(mapWidth + 1, mapHeight + 1);

            Clear(); //очистка экрана
            GameBorderDrawer.DrawBorder(mapWidth, mapHeight); //отрисовка границ игрового поля

            Direction currentMovement = Direction.Right; //начальное направление движения змейки
            var snake = new Snake(10, 5, Configurations.HeadColor, Configurations.BodyColor); //создание змейки

            Pixel food = FoodGenerator.GenFood(snake, mapWidth, mapHeight, Configurations.FoodColor); // Генерация еды
            food.Draw(); //отрисовка еды

            Pixel? bomb = withBomb ? BombGenerator.GenBomb(snake, food, mapWidth, mapHeight, Configurations.BombColor) : (Pixel?)null; // Генерация бомбы
            bomb?.Draw(); //отрисовка бомбы

            int score = 0;
            int maxScore = (mapWidth - 2) * (mapHeight - 2) - snake.Body.Count - 1; //максимальный возможный счет
            Stopwatch sw = new Stopwatch();

            while (true)
            {
                sw.Restart();
                Direction oldMovement = currentMovement;

                while (sw.ElapsedMilliseconds <= Configurations.FrameMs)
                {
                    if (currentMovement == oldMovement)
                    {
                        currentMovement = ReadMovement(currentMovement);
                    }
                }

                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(currentMovement, true); //змейка съедает еду
                    food = FoodGenerator.GenFood(snake, mapWidth, mapHeight, Configurations.FoodColor); //новая еда
                    food.Draw();

                    if (withBomb)
                    {
                        bomb?.Clear();
                        bomb = BombGenerator.GenBomb(snake, food, mapWidth, mapHeight, Configurations.BombColor); //новая бомба
                        bomb?.Draw();
                    }

                    score++;

                    Task.Run(() => Beep(1200, 200)); //звук при съедании еды

                    if (score == maxScore)
                    {
                        OnGameWin?.Invoke(score); //события победы
                        break;
                    }
                }
                else if (withBomb && bomb.HasValue && snake.Head.X == bomb.Value.X && snake.Head.Y == bomb.Value.Y)
                {
                    OnGameLoss?.Invoke(score); //проигрыш при столкновении с бомбой
                    HandleLoss(score);
                    break;
                }
                else
                {
                    snake.Move(currentMovement); //движение змейки
                }

                //проверка на столкновение с границей или своим телом
                if (snake.Head.X == mapWidth - 1
                   || snake.Head.X == 0
                   || snake.Head.Y == mapHeight - 1
                   || snake.Head.Y == 0
                   || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                {
                    OnGameLoss?.Invoke(score); //проигрыш
                    HandleLoss(score);
                    break;
                }
            }

            snake.Clear();
            food.Clear();
            bomb?.Clear();
        }

        public void Handle(GameContext context)
        {
            Run(); 
        }

        private void HandleLoss(int score)
        {
            // Переключение в состояние GameOver при проигрыше
            var context = new GameContext(new GameOverState(score));
            context.Request();
        }

        private void HandleWin(int score)
        {
            // Здесь можно реализовать логику для победы (например, переход в состояние победы)
            var context = new GameContext(new GameOverState(score)); // Пока что используем экран проигрыша
            context.Request();
        }


        public Direction ReadMovement(Direction currentDirection)
        {
            if (!Console.KeyAvailable)
                return currentDirection;

            ConsoleKey key = Console.ReadKey(true).Key;

            currentDirection = key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection
            };

            return currentDirection;
        }
    }
}