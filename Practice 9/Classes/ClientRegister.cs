using System;
using System.Collections.Generic;
using Practice_9.Drawing;
using Newtonsoft.Json;
using System.IO;

namespace Practice_9.Classes
{
    public static class ClientRegister
    {
        private static Point origin = new (Program.WIDTH/2,Program.HEIGHT/2);
        private static Window _window= Program.win;
        private static int element_choosen = 0;
        private static string[] strs = new string[5] {"","","","",""};
        private static List<string> strings = new List<string>();
        private static bool err = false;

        private static void Clear()
        {
            for (int i = 0; i < 5; i++)
            {
                strs[i] = "";
            }
            strings.Clear();
            err = false;
            element_choosen = 0;
        }

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
            _window.drawString(new Point(origin.x-10,origin.y+strings.Count), $"When all fields are filled correctly, press [ENTER]. If you want to log in, press Escape");
            if(err) _window.drawString(new Point(origin.x-10,origin.y+strings.Count+1), $"Invalid login/password/phone");
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
            Draw();
            while (!end)
            {
                Console.SetCursorPosition(0,0);
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
                            foreach (var us in Program.users.ToArray())
                            {
                                if (us.login == strs[1] || us.email == strs[3] || us.phone == strs[4])
                                {
                                    err = true;
                                }
                            }

                            if (!err)
                            {
                                
                                Program.users.Add(new User(strs[0],DateTime.Today,Role.CLIENT,strs[1],strs[2],strs[3],strs[4]));
                                string outer = JsonConvert.SerializeObject(Program.users);
                                File.WriteAllText("lol.json",outer);
                                end = true;      
                            }

                            break;
                    case ConsoleKey.F1:
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
                Draw();
            }
            Clear();
        }


    }
}