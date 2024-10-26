using SnakeGame.Models;

namespace SnakeGame.Helpers
{
    public static class BombGenerator
    {
        private static readonly Random Random = new Random();

        public static Pixel GenBomb(Snake snake, Pixel food, int mapWidth, int mapHeight, ConsoleColor bombColor)
        {
            Pixel bomb;
            do
            {
                bomb = new Pixel(Random.Next(2, mapWidth - 2), Random.Next(1, mapHeight - 1), bombColor, '*');
            } while (snake.Body.Any(b => b.X == bomb.X && b.Y == bomb.Y ||
                                         (food.X == bomb.X && food.Y == bomb.Y)));

            return bomb;
        }
    }
}
