using System;
using System.Collections.Generic;
using System.Threading;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{

    public class AccountantPanel
    {
        private static Point origin = new(Program.WIDTH / 2, Program.HEIGHT / 2);
        private static Window _window = Program.win;
        private static List<string> strings = new List<string>();
        private static int element_choosen = 0;

        public static void main()
        {
            _window.clearBuffer();
            controls();
            
        }
        public static void Draw()
        {
            _window.clearBuffer();
            for (int i = 0; i < strings.Count; i++)
            {
                if (i == element_choosen)
                    _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + strings[i]);
                else
                    _window.drawString(new Point(origin.x - 10, origin.y + i), strings[i]);
            }
            _window.drawBuffer();
        }

        public static void controls()
        {
            strings.Add($"Доходы сотрудников");
            strings.Add($"Доходы за покупки");
            strings.Add($"Общий бюджет");
            Draw();
            bool end = false;
            while (!end)
            {
                Console.SetCursorPosition(0, 0);
                Draw();
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
                    case ConsoleKey.Backspace:

                        break;
                    case ConsoleKey.Enter:
                        if (element_choosen == 0)
                        {
                            getSlary();
                        }

                        //if (element_choosen == 1)
                        {
                            
                        }

                        //if (element_choosen == 2)
                        {
                            
                        }
                        break;
                    case ConsoleKey.Escape:
                        end = true;
                        break;
                    default:
                        char ch = ' ';
                        ch = key.KeyChar;
                        Console.Write(ch);
                        break;

                }
            }
        }
        public static void getSlary()
        {
            foreach (var a in Program.users)
            {
                Console.WriteLine(a.login);
            }
            var getLogin = Convert.ToString(Console.ReadLine());
            foreach (var a in Program.users)
            {
                if (getLogin.Equals(a.login))
                {
                    _window.clearBuffer();
                    _window.drawString(new Point(origin.x - 118, origin.y - 30), 
                        $"Месячная зарплата | {a.salary} | Зарплата за пол года {a.salary * 6} | Зарплата за года {a.salary * 12} | Зарплата за все время работы {a.salary}");
                }
                _window.drawBuffer();
            }
            Console.ReadKey();
        }
    }
}