using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{

    public class AccountantPanel
    {
        private static Point origin = new(Program.WIDTH / 2, Program.HEIGHT / 2);
        private static Window _window = Program.win;
        private static List<string> strings = new List<string>();
        private static List<string> workerStrings = new List<string>();
        private static int element_choosen = 0;
        private static bool defaultdraw = true;
        private static bool drawGetSalary = false;
        private static ConsoleKeyInfo key;
        private static int i = 0;

        public static void main()
        {
            if (!WorkerSetup.main())
            {
                Program.currrentUser = null;
                return;
            }
            _window.clearBuffer();
            controls();
        }
        public static void Draw()
        {
            _window.clearBuffer();
            if (defaultdraw == true)
            {
                for (int i = 0; i < strings.Count; i++)
                {
                    if (i == element_choosen)
                        _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + strings[i]);
                    else
                        _window.drawString(new Point(origin.x - 10, origin.y + i), strings[i]);
                }
                _window.drawBuffer();
            }
            if (drawGetSalary == true)
            {
                _window.clearBuffer();
                
                //foreach (var a in Program.users)
                //{
                    for (int ii = 0; ii < workerStrings.Count; ii++)
                    {
                        //if (i == element_choosen)
                        // _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + workerStrings[i]);
                        //else
                        _window.drawString(new Point(origin.x - 118, origin.y - 30 + ii), $"{ii} | {delSpace(workerStrings[ii], 20)} " +
                            $"| Месячная зарплата   {delSpace(Program.users[ii].role.salary.ToString(), 10)} | Зарплата за пол года   {delSpace((Program.users[ii].role.salary * 6).ToString(), 10)} " +
                            $"| Зарплата за год   {delSpace((Program.users[ii].role.salary * 12).ToString(), 10)} | Зарплата за все время работы   {Program.users[ii].role.salary*Math.Round( (DateTime.Today.Subtract(Program.users[ii].regDate).Days / (365.25 / 12)),4)}");
                        
                        _window.drawBuffer();
                    }
                    _window.drawBuffer();
                    
                //}
                drawGetSalary = false;
                defaultdraw = true;
            }
        }
        public static void controls()
        {
            strings.Add($"Доходы сотрудников");
            strings.Add($"Доходы за покупки");
            strings.Add($"Общий бюджет");
            strings.Add("Установить зарплату");
            begin:
            Draw();
            bool end = false;
            while (!end)
            {
                Console.SetCursorPosition(0, 0);
                Draw();
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (element_choosen == 0) element_choosen = strings.Count - 1;
                        else element_choosen--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (element_choosen == strings.Count - 1) element_choosen = 0;
                        else element_choosen++;
                        break;
                    case ConsoleKey.Backspace:
                        goto begin;
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
                        Program.currrentUser = null;
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
            if (i == 0)
            {
                foreach (var a in Program.users)
                {
                    if(a.role.ID<5)
                    workerStrings.Add(a.login);
                }
            }
            drawGetSalary = true;
            defaultdraw = false;
            i++;
        }

        public static string delSpace(string raw, int max)
        {
            string a = "";
            for (int j = 0; j < max; j++)
            {
                if (raw.Length > j)
                {
                    var ur = raw[j];
                    a += ur;
                }
                else
                {
                    a += " ";
                }
            }
            return a;
        }
    }
}