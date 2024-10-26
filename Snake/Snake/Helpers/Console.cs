namespace SnakeGame.Helpers
{
    //метод для управления положением окна консоли на экране
    internal class ConsoleHelper
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern nint GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(nint hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private const int ScreenWidth = 1920; //ширина экрана
        private const int ScreenHeight = 1080; //высота экрана

        //метод для центрирования окна консоли
        public void CenterConsoleWindow()
        {
            nint consoleWindow = GetConsoleWindow();
            if (consoleWindow == nint.Zero) return;

            GetWindowRect(consoleWindow, out RECT rect);
            int windowWidth = rect.Right - rect.Left;
            int windowHeight = rect.Bottom - rect.Top;

            int posX = (ScreenWidth - windowWidth) / 2;
            int posY = (ScreenHeight - windowHeight) / 2;

            MoveWindow(consoleWindow, posX, posY, windowWidth, windowHeight, true);
        }

        public void SetConsoleSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.CursorVisible = false;
        }
    }
}