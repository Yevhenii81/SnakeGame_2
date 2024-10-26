using SnakeGame.Models;
using SnakeGame.GameState;

namespace SnakeGame.Helpers
{
    public static class FoodGenerator
    {
        private static readonly Random Random = new Random();
        public static Pixel GenFood(Snake snake, int mapWidth, int mapHeight, ConsoleColor foodColor)
        {
            Pixel food;
            do
            {
                food = new Pixel(Random.Next(2, mapWidth - 2), Random.Next(1, mapHeight - 1), foodColor, 'o');
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y ||
                     snake.Body.Any(b => b.X == food.X && b.Y == food.Y));

            return food;
        }
    }
}
