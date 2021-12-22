﻿using System;
using System.Collections.Generic;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{
    public static class WorkerSetup
    {
        private static User worker = null;
        private static Window _window = Program.win;
        private static List<string> menu = new List<string>();
        private static int pointer = 0;
        private static bool noAbort = true;
        static string name = "";
        static string date = "";

        /// <summary>
        /// Завершение регистрации пользователя.
        /// Необходимо использовать в основных методах панелей работников в формате
        /// if (!WorkerSetup.main()) return;
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
            menu.Add("Дата рождения: ");//15 chars
            head();
            draw();
            controls();
            return noAbort;
        }



        private static void head()
        {
            for (int i=0;i<Console.WindowWidth;i++)
            {
                _window.drawDot(new(i,1),'═');
            }
            _window.drawString(new (Program.WIDTH/2-20,0),$"Завершение регистрации сотрудника под логином {worker.login}");
            _window.drawBuffer();
        }

        private static void controls()
        {
            bool end = false;
            while (!end)
            {
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
                            if (DateTime.Today > Convert.ToDateTime(date))
                                end = true;
                            worker.Birthday = Convert.ToDateTime(date);
                            worker.Name = name;
                        }
                        catch (Exception e)
                        {
                            //Here it goes! Nothing.
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (pointer == 0) name = name.Remove(name.Length - 1);
                        else date = date.Remove(date.Length - 1);
                        break;
                    default:
                        char ch = key.KeyChar;
                        if (pointer == 0) name += ch;
                        else
                        {
                            if (date.Length == 3 || date.Length == 7)
                            {
                                date += "/" + ch;
                            }
                            else
                                date += ch;
                        }
                        break;
                }
                draw();
            }
        }

        private static void draw()
        {
            _window.clearBuffer();
            head();
            if (pointer == 0)
            {
                _window.drawString(new (Program.WIDTH/2,Program.HEIGHT/2),$">> {menu[0]}{name}");
                
                _window.drawString(new (Program.WIDTH/2,Program.HEIGHT/2),$"   {menu[1]}{date}");
            }
            else
            {
                _window.drawString(new (Program.WIDTH/2,Program.HEIGHT/2),$"   {menu[0]}{name}");
                
                _window.drawString(new (Program.WIDTH/2,Program.HEIGHT/2),$">> {menu[1]}{date}");
            }
            _window.drawBuffer();
        }
    }
}