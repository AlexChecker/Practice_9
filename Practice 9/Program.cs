using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using Practice_9.Classes;
using Practice_9.Drawing;

namespace Practice_9
{
    internal class Program
    {
        
        [DllImport("kernel32.dll", ExactSpelling = true)]  
        private static extern IntPtr GetConsoleWindow();  
        private static IntPtr ThisConsole = GetConsoleWindow();  
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);  
        private const int HIDE = 0;  
        private const int MAXIMIZE = 3;  
        private const int MINIMIZE = 6;  
        private const int RESTORE = 9;

        public static int WIDTH = Console.LargestWindowWidth;//Console.LargestWindowWidth / 2;
        public static int HEIGHT = Console.LargestWindowHeight;//Console.LargestWindowHeight / 2;
        public static Window win = new Window(WIDTH,HEIGHT);

        public static List<User> users = new List<User>();
        public static void Main(string[] args)
        {
            User debugAdmin = new User("Alex",DateTime.Today, Role.ADMIN,"admin","admin");
            users.Add(debugAdmin);
            
            Console.Title = "Practice 9";
            Console.OutputEncoding = Encoding.Unicode;
            ShowWindow(ThisConsole, MAXIMIZE);
            ClientRegister.controls();
            win.clearBuffer();
            foreach (User us in users)
            {
                Console.WriteLine(us.login + " "+ us.password+" "+us.role.name);             
            }

            Console.ReadKey(true);
        }
    }
}