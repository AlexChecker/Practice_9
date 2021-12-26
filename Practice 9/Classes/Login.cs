using System;
using System.Collections.Generic;
using System.Threading;
using Practice_9.Drawing;

namespace Practice_9.Classes
{
    public static class Login
    {
        private static Point origin = new Point(Program.WIDTH/2,Program.HEIGHT/2);
        private static Window _window= Program.win;
        private static int element_choosen = 0;
        private static string[] strs = new string[2] {"",""};
        private static List<string> strings = new List<string>();
        public static void Draw()
        {
            _window.clearBuffer();
            for (int i = 0; i < strings.Count; i++)
            {
                if (i==element_choosen)
                    _window.drawString(new Point(origin.x-13,origin.y+i),">> "+strings[i]);
                else
                    _window.drawString(new Point(origin.x-10,origin.y+i),strings[i]);
            }
            _window.drawString(new Point(origin.x-10,origin.y+strings.Count), $"When all fields are filled correctly, press [ENTER]. If you want to register, press Escape");
            _window.drawBuffer();
        }

        public static void clear()
        {
            strings.Clear();
            strs[0] = "";
            strs[1] = "";
            element_choosen = 0;
        }
        public static void controls()
        {
            strings.Add($"Login: {strs[0]}");
            strings.Add($"Password: {strs[1]}");
            bool end = false;
            while (!end)
            {
                Console.SetCursorPosition(0,0);
                Draw();
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (element_choosen == 0) element_choosen = 1;
                        else element_choosen--;
                        break;
                    
                    case ConsoleKey.DownArrow:
                        if (element_choosen == 1) element_choosen = 0;
                        else element_choosen++;
                        break;
                    case ConsoleKey.Backspace:
                        if (strs[element_choosen].Length != 0)
                        {
                            strs[element_choosen] = strs[element_choosen].Remove(strs[element_choosen].Length - 1);
                        }
                        break;
                    case ConsoleKey.Enter:
                        foreach (User us in Program.users.ToArray())
                        {
                            if (strs[0] == us.login && strs[1] == us.password)
                            {
                                Program.currrentUser = us;
                                end = true;
                                strings.Clear();
                            }

                            if (strs[0] != us.login || strs[1] != us.password)
                            {
                                _window.drawString(new Point(origin.x - 10, origin.y + strings.Count + 1), $"Введите коректные данные");
                                _window.drawBuffer();
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        end = true;
                        break;
                    default:
                        char ch = ' ';
                        ch = key.KeyChar;
                        Console.Write(ch);
                        strs[element_choosen] += ch;
                        
                        break;
                }
                
                strings.Clear();
                strings.Add($"Login: {strs[0]}");
                strings.Add($"Password: {strs[1]}");
            }
            clear();
        }
    }
}