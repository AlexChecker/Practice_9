using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{
    public static class WorkerSetup
    {
        private static User worker;
        private static readonly Window _window = Program.win;
        private static readonly List<string> menu = new();
        private static int pointer;
        private static bool noAbort = true;
        private static string name = "";
        private static string date = "";
        private static bool err;

        /// <summary>
        ///     Завершение регистрации пользователя.
        ///     Необходимо использовать в основных методах панелей работников в формате
        ///     if (!WorkerSetup.main()) return;
        /// </summary>
        /// <returns>true, если пользователь не отказался от регистрации</returns>
        public static bool main()
        {
            name = "";
            date = "";
            noAbort = true;
            pointer = 0;
            worker = Program.currrentUser;
            menu.Clear();
            menu.Add("ФИО:           ");
            menu.Add("Дата рождения: "); //15 chars
            head();
            draw();
            controls();
            return noAbort;
        }

        private static void draw()
        {
            _window.clearBuffer();
            head();
            if (pointer == 0)
            {
                _window.drawString(new Point(Program.WIDTH / 2 - 10, Program.HEIGHT / 2), $">> {menu[0]}{name}");

                _window.drawString(new Point(Program.WIDTH / 2 - 10, Program.HEIGHT / 2 - 1), $"   {menu[1]}{date}");
            }
            else
            {
                _window.drawString(new Point(Program.WIDTH / 2 - 10, Program.HEIGHT / 2), $"   {menu[0]}{name}");

                _window.drawString(new Point(Program.WIDTH / 2 - 10, Program.HEIGHT / 2 - 1), $">> {menu[1]}{date}");
            }

            if (err) _window.drawString(new Point(Program.WIDTH / 2 + 3, Program.HEIGHT / 2 + 1), "Ошибочка вышла)");
            _window.drawBuffer();
        }

        private static void head()
        {
            for (var i = 0; i < Console.WindowWidth; i++) _window.drawDot(new Point(i, 1), '═');
            _window.drawString(new Point(Program.WIDTH / 2 - 20, 0),
                $"Завершение регистрации сотрудника под логином {worker.login}");
            _window.drawBuffer();
        }

        private static void controls()
        {
            var end = false;
            while (!end)
            {
                Console.SetCursorPosition(0, 0);
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (pointer == 0) pointer = 1;
                        else pointer = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        if (pointer == 1) pointer = 0;
                        else pointer = 1;
                        break;
                    case ConsoleKey.Escape:
                        noAbort = false;
                        end = true;
                        Program.currrentUser = null;
                        break;
                    case ConsoleKey.Enter:
                        try
                        {
                            if (DateTime.Today > Convert.ToDateTime(date)) end = true;

                            worker.Birthday = Convert.ToDateTime(date);
                            worker.Name = name;
                            var outer = JsonConvert.SerializeObject(Program.users);
                            File.WriteAllText("users.json", outer);
                        }
                        catch
                        {
                            err = true;
                        }

                        break;

                    case ConsoleKey.Backspace:
                        try
                        {
                            if (pointer == 0) name = name.Remove(name.Length - 1);
                            else date = date.Remove(date.Length - 1);
                        }
                        catch (Exception)
                        {
                            //ignored
                        }

                        break;
                    default:
                        var ch = key.KeyChar;
                        if (pointer == 0)
                        {
                            name += ch;
                        }
                        else
                        {
                            if (date.Length == 2 || date.Length == 5)
                                date += "/" + ch;
                            else if (date.Length < 10)
                                date += ch;
                        }

                        break;
                }

                draw();
            }
        }
    }
}