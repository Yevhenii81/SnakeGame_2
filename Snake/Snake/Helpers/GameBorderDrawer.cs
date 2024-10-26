using SnakeGame.Models;
using SnakeGame.Helpers;
using SnakeGame.Utilities;

namespace SnakeGame.Helpers
{
    public static class GameBorderDrawer
    {
        public static void DrawBorder(int mapWidth, int mapHeight)
        {
            //отрисовка верхней границы
            for (int i = 0; i <= mapWidth; i++)
            {
                new Pixel(i, 0, Configurations.BorderColor).Draw(); //верхний ряд
            }

            // Отрисовка боковых границ
            for (int i = 1; i <= mapHeight; i++)
            {
                new Pixel(0, i, Configurations.BorderColor).Draw(); //левая граница
                new Pixel(1, i, Configurations.BorderColor).Draw(); //вторая левая граница
                new Pixel(mapWidth, i, Configurations.BorderColor).Draw(); //правая граница
                new Pixel(mapWidth - 1, i, Configurations.BorderColor).Draw(); //вторая правая граница
            }

            //отрисовка нижней границы
            for (int i = 0; i <= mapWidth; i++)
            {
                new Pixel(i, mapHeight, Configurations.BorderColor).Draw(); //нижний ряд
            }
        }
    }
}