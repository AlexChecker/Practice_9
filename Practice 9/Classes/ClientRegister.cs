using System;
using System.Collections.Generic;
using Practice_9.Drawing;

namespace Practice_9.Classes
{
    public static class ClientRegister
    {
        private static Point origin = new (Program.WIDTH/2,Program.HEIGHT/2);
        private static Window _window= Program.win;
        private static int element_choosen = 0;
        private static string[] strs = new string[5] {"","","","",""};
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
            _window.drawString(new Point(origin.x-10,origin.y+5), $"When all fields are filled correctly, press [ENTER]");
            _window.drawBuffer();
        }

        public static void controls()
        {
            
            strings.Add($"Name: {strs[0]}");
            strings.Add($"Login: {strs[1]}");
            strings.Add($"Password: {strs[2]}");
            strings.Add($"Email: {strs[3]}");
            strings.Add($"Phone: {strs[4]}");
            bool end = false;
            while (!end)
            {
                Console.SetCursorPosition(0,0);
                Draw();
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (element_choosen == 0) element_choosen = 4;
                        else element_choosen--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (element_choosen == 4) element_choosen = 0;
                        else element_choosen++;
                        break;
                    case ConsoleKey.Backspace:
                        if (strs[element_choosen].Length != 0)
                        {
                            strs[element_choosen] = strs[element_choosen].Remove(strs[element_choosen].Length - 1);
                        }

                        break;
                    case ConsoleKey.Enter:
                        Program.users.Add(new User(strs[0],DateTime.Today,Role.CLIENT,strs[1],strs[2],strs[3],strs[4]));
                        end = true;
                        break;
                    default:
                        char ch = ' ';
                        try
                        {
                            ch = key.KeyChar;
                            Console.Write(ch);
                        }
                        catch (Exception e)
                        {

                        }

                        strs[element_choosen] += ch;
                        break;
                }
                strings.Clear();
                strings.Add($"Name: {strs[0]}");
                strings.Add($"Login: {strs[1]}");
                strings.Add($"Password: {strs[2]}");
                strings.Add($"Email: {strs[3]}");
                strings.Add($"Phone: {strs[4]}");
            }
        }


    }
}