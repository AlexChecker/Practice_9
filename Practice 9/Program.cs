using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Practice_9.Classes;
using Practice_9.Drawing;

namespace Practice_9
{
    internal class Program
    {
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        private static readonly IntPtr ThisConsole = GetConsoleWindow();

        public static int WIDTH = Console.LargestWindowWidth; //Console.LargestWindowWidth / 2;
        public static int HEIGHT = Console.LargestWindowHeight; //Console.LargestWindowHeight / 2;
        public static Window win = new(WIDTH, HEIGHT);

        public static List<User> users = new();
        public static User currrentUser;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Main(string[] args)
        {
            var debugAdmin = new User("Alex", DateTime.Today, Role.ADMIN, "admin", "admin", null, null);
            var debugOperator = new User("Alex", DateTime.Today, Role.OPERATOR, "op", "op", null, null);
            var debugHR = new User("Alex", DateTime.Today, Role.HR, "hr", "hr", null, null);
            var debugAccountant = new User("Alex", DateTime.Today, Role.ACCOUNTANT, "ac", "ac", null, null);
            var debugManager = new User("Alex", DateTime.Today, Role.MANAGER, "mn", "mn", null, null);
            users.Add(debugAdmin);
            users.Add(debugOperator);
            users.Add(debugHR);
            users.Add(debugAccountant);
            users.Add(debugManager);

            Console.Title = "Practice 9";
            Console.OutputEncoding = Encoding.Unicode;
            ShowWindow(ThisConsole, MAXIMIZE);

            if (!File.Exists("users.json")) ClientRegister.controls();

            users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("users.json"));
            StartScreen.controls();
        }
    }
}