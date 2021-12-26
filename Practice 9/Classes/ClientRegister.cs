using System;
using System.Collections.Generic;
using Practice_9.Drawing;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace Practice_9.Classes
{
    public static class ClientRegister
    {
        

        //середина экрана
        private static Point origin = new Point(Program.WIDTH / 2, Program.HEIGHT / 2);

        //окно для отрисовки
        private static Window _window = Program.win;

        //выбранный элемент
        private static int element_choosen = 0;

        private static string[] strs = new string[6] {"", "", "", "", "", ""};
        private static List<string> strings = new List<string>();
        private static bool err = false;
        private static string mask = "";


        private static void Clear()
        {
            for (int i = 0; i < 6; i++)
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
                if (i == element_choosen)
                    _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + strings[i]);
                else
                    _window.drawString(new Point(origin.x - 10, origin.y + i), strings[i]);
            }
            
            _window.drawString(new Point(origin.x - 10, origin.y + strings.Count),
                $"When all fields are filled correctly, press [ENTER]. If you want to log in, press Escape");
            if (err)
                _window.drawString(new Point(origin.x - 10, origin.y + strings.Count + 1),
                    $"Invalid login/password/phone");
            _window.drawBuffer();
        }

        public static void controls()
        {
            //bool mail = false;
            strings.Add($"Name: {strs[0]}");
            strings.Add($"Login: {strs[1]}");
            strings.Add($"Password: {mask}");
            strings.Add($"Email: {strs[3]}");
            strings.Add($"Phone: +7 {strs[4]}");
            strings.Add($"Birthday: {strs[5]}");
            bool end = false;
            Draw();
            while (!end)
            {
                Console.SetCursorPosition(0, 0);
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (element_choosen == 0) element_choosen = 5;
                        else element_choosen--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (element_choosen == 5) element_choosen = 0;
                        else element_choosen++;
                        break;
                    case ConsoleKey.Backspace:
                        if (strs[element_choosen].Length != 0)
                        {
                            strs[element_choosen] = strs[element_choosen].Remove(strs[element_choosen].Length - 1);
                        }

                        if (element_choosen == 2 && mask.Length != 0)
                            mask = mask.Remove(mask.Length - 1);

                        break;
                    //Отправляет юзера на регистрацию
                    case ConsoleKey.Enter:
                        var letter = 0;
                        var digit = 0;
                        foreach (var us in Program.users.ToArray())
                        {
                            if (us.login == strs[1] || us.email == strs[3] || us.phone == strs[4])
                            {
                                err = true;
                            }
                        }

                        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
                        if (hasSymbols.Matches(strs[2]).Count < 2)
                        {
                            err = true;
                        }

                        foreach (var i in strs[2].ToCharArray())
                        {
                            if (Char.IsDigit(i))
                            {
                                digit++;
                            }
                            else
                            {
                                letter++;
                            }
                        }

                        if (letter < 3 || digit < 3)
                        {
                            err = true;
                        }

                        strs[4] = strs[4].Replace("/", "");
                        try
                        {
                            if (strs[4].Length < 9 || strs[2].Length <= 8 ||
                                DateTime.Today < Convert.ToDateTime(strs[5]))// || !strs[3].Contains("@"))
                            {
                                err = true;
                            }
                        }
                        catch
                        {
                            err = true;
                        }

                        try
                        {
                            Convert.ToInt64(strs[4]);
                            Convert.ToDateTime(strs[5]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Console.WriteLine("Obser tut");
                            Console.ReadKey();
                            err = true;
                        }

                        if (err == true)
                        {
                            //добавить отчистку strs[4] если ошибка вылетает
                            for (int i = 0; i < strs.Length; i++)
                            {
                                strs[i] = "";
                            }
                        }

                        if (!err)
                        {
                            Program.users.Add(new User(strs[0], Convert.ToDateTime(strs[5]), Role.CLIENT, strs[1],
                                strs[2], strs[3], "+7" + strs[4]));
                            string outer = JsonConvert.SerializeObject(Program.users);
                            File.WriteAllText("users.json", outer);
                            end = true;
                        }

                        //err = false;
                        break;
                    case ConsoleKey.F1:
                        break;
                    case ConsoleKey.Escape:
                        end = !File.Exists("user.json");
                        break;
                    default:
                        char ch = ' ';
                        ch = key.KeyChar;
                        //Console.Write(ch);


                        //if (element_choosen == 4)
                        //{
                        //    if (int.TryParse(ch.ToString(),out int r))
                        //    {
                        //        strs[element_choosen] += ch;
                        //    }
                        //()}
                        if (element_choosen == 0 && strs[0].Length < 30)
                        {
                            strs[0] += ch;
                        }

                        if (element_choosen == 1 && strs[1].Length < strs[3].Length)
                        {
                            strs[1] += ch;
                        }

                        if (element_choosen == 2)
                        {
                            strs[2] += ch;
                            mask += '*';
                        }

                        if (element_choosen == 3)
                        {
                            strs[3] += ch;
                        }

                        if (element_choosen == 4 && strs[4].Length < 11)
                        {
                            if (strs[4].Length == 3 || strs[4].Length == 7)
                            {
                                strs[4] += "/" + ch;
                            }
                            else
                                strs[4] += ch;
                        }

                        if (element_choosen == 5 && strs[5].Length <= 9)
                        {
                            if (strs[5].Length == 2 || strs[5].Length == 5)
                            {
                                strs[5] += "/" + ch;
                            }
                            else
                                strs[5] += ch;
                        }
                        //if (element_choosen != 5 || element_choosen != 4)
                        //strs[element_choosen] += ch;

                        break;
                    //&& element_choosen != 2 &&element_choosen !=4 &&element_choosen != 5
                }

                strings.Clear();
                strings.Add($"Name: {strs[0]}");
                strings.Add($"Login: {strs[1]}");
                strings.Add($"Password: {strs[2]}");
                strings.Add($"Email: {strs[3]}");
                strings.Add($"Phone: +7 {strs[4]}");
                strings.Add($"Birthday: {strs[5]}");
                Draw();
            }

            Clear();
        }
    }
}