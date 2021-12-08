using System;
using Practice_9.Drawing;

namespace Practice_9.Classes
{
    public static class StartScreen
    {
        private static Point origin = new(Program.WIDTH / 2, Program.HEIGHT / 2);
        private static string[] strs = {"Log in", "Register", "Exit"};
        private static int element_choosen = 0;
        private static Window _window = Program.win;

        private static void draw()
        {
            _window.clearBuffer();
            for (int i = 0; i < 3; i++)
            {
                if (i == element_choosen)
                    _window.drawString(new Point(origin.x - 13, origin.y + i), "[" + strs[i] + "]");
                else
                {
                    _window.drawString(new Point(origin.x - 13, origin.y + i), strs[i] );
                }
            }
            _window.drawBuffer();
        }

        public static void controls()
        {
            bool end = false;
            while (!end)
            {
                draw();
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (element_choosen == 0) element_choosen = 2;
                        else element_choosen--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (element_choosen == 2) element_choosen = 0;
                        else element_choosen++;
                        break;
                    case ConsoleKey.Enter:
                        if(element_choosen == 0) Login.controls();
                        else if(element_choosen == 1) ClientRegister.controls();
                        else Environment.Exit(0);

                        break;
                }
            }
        }
    }
}