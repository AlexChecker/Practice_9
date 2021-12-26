using System;
using System.Collections.Generic;
using Practice_9.Drawing;
using static System.ConsoleKey;

namespace Practice_9.Classes.Panels
{
    public class AccountantPanel
    {
        private static readonly Point origin = new(Program.WIDTH / 2, Program.HEIGHT / 2);
        private static readonly Window _window = Program.win;
        private static readonly List<string> strings = new();
        private static readonly List<string> workerStrings = new();
        private static int element_choosen;
        private static int worker_choosen;
        private static bool defaultdraw = true;
        private static bool drawGetSalary;
        private static bool drawWorker;
        private static int i;

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
            if (defaultdraw)
            {
                for (var i = 0; i < strings.Count; i++)
                    if (i == element_choosen)
                        _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + strings[i]);
                    else
                        _window.drawString(new Point(origin.x - 10, origin.y + i), strings[i]);
                _window.drawBuffer();
            }

            if (drawGetSalary)
            {
                _window.clearBuffer();

                //foreach (var a in Program.users)
                //{
                for (var ii = 0; ii < workerStrings.Count; ii++)
                {
                    //if (i == element_choosen)
                    // _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + workerStrings[i]);
                    //else
                    _window.drawString(new Point(origin.x - 118, origin.y - 30 + ii),
                        $"{ii} | {delSpace(workerStrings[ii], 20)} " +
                        $"| Месячная зарплата   {delSpace(Program.users[ii].role.salary.ToString(), 10)} | Зарплата за пол года   {delSpace((Program.users[ii].role.salary * 6).ToString(), 10)} " +
                        $"| Зарплата за год   {delSpace((Program.users[ii].role.salary * 12).ToString(), 10)} | Зарплата за все время работы   {Program.users[ii].role.salary * Math.Round(DateTime.Today.Subtract(Program.users[ii].regDate).Days / (365.25 / 12), 4)}");

                    _window.drawBuffer();
                }

                _window.drawBuffer();
                //}
                drawGetSalary = false;
                defaultdraw = true;
            }

            if (drawWorker)
            {
                _window.clearBuffer();
                for (var i = 0; i < workerStrings.Count; i++)
                {
                    if (i == worker_choosen)
                        _window.drawString(new Point(origin.x - 118, origin.y - 30 + i),
                            $"[{delSpace(workerStrings[i], 20)}{Program.users[i].role.salary}]");
                    else
                        _window.drawString(new Point(origin.x - 118, origin.y - 30 + i),
                            $"{delSpace(workerStrings[i], 20)}{Program.users[i].role.salary}");

                    _window.drawBuffer();
                }

                _window.drawBuffer();
                drawWorker = false;
                defaultdraw = true;
            }
        }

        public static void controls()
        {
            strings.Add("Доходы сотрудников");
            strings.Add("Доходы за покупки");
            strings.Add("Общий бюджет");
            strings.Add("Установить зарплату");
            begin:
            var key = Console.ReadKey();
            Draw();
            var end = false;
            while (!end)
            {
                Console.SetCursorPosition(0, 0);
                Draw();
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case UpArrow:
                        if (element_choosen == 0) element_choosen = strings.Count - 1;
                        else element_choosen--;
                        break;

                    case DownArrow:
                        if (element_choosen == strings.Count - 1) element_choosen = 0;
                        else element_choosen++;
                        break;
                    case Backspace:
                        goto begin;
                        break;
                    case Enter:
                        if (element_choosen == 0) writeSalary();
                        //if (element_choosen == 1)
                    {
                    }

                        //if (element_choosen == 2)
                    {
                    }
                        if (element_choosen == 3) getSalary();
                        break;
                    case Escape:
                        strings.Clear();
                        end = true;
                        Program.currrentUser = null;
                        break;
                    default:
                        var ch = ' ';
                        ch = key.KeyChar;
                        //Console.Write(ch);
                        //Console.ReadKey();
                        break;
                }
            }
        }

        public static void writeSalary()
        {
            if (i == 0)
                foreach (var a in Program.users)
                    if (a.role.ID < 5)
                        workerStrings.Add(a.login);
            drawGetSalary = true;
            defaultdraw = false;
            i++;
        }

        public static void getSalary()
        {
            workerStrings.Clear();
            foreach (var item in Program.users)
                if (item.role.ID < 5)
                    workerStrings.Add($"{item.login}");
            drawWorker = true;
            var end = true;
            while (end)
            {
                var keyWorker = Console.ReadKey();
                switch (keyWorker.Key)
                {
                    case UpArrow:
                        if (worker_choosen == 0) worker_choosen = workerStrings.Count - 1;
                        else worker_choosen--;
                        break;

                    case DownArrow:
                        if (worker_choosen == workerStrings.Count - 1) worker_choosen = 5;
                        else worker_choosen++;
                        break;
                    case Escape:
                        drawWorker = false;
                        end = false;
                        break;
                }
            }
        }

        public static string delSpace(string raw, int max)
        {
            var a = "";
            for (var j = 0; j < max; j++)
                if (raw.Length > j)
                {
                    var ur = raw[j];
                    a += ur;
                }
                else
                {
                    a += " ";
                }

            return a;
        }
    }
}