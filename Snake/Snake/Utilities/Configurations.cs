namespace SnakeGame.Utilities
{
    internal class Configurations
    {
        // Время кадра в миллисекундах
        public const int FrameMs = 200;

        // Цвета для границ, головы и тела змеи, еды и бомбы
        public static readonly ConsoleColor BorderColor = ConsoleColor.DarkRed;
        public static readonly ConsoleColor HeadColor = ConsoleColor.DarkGreen;
        public static readonly ConsoleColor BodyColor = ConsoleColor.Green;
        public static readonly ConsoleColor FoodColor = ConsoleColor.Yellow;
        public static readonly ConsoleColor BombColor = ConsoleColor.Red;
    }
}