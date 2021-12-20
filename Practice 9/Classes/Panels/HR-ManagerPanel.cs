using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{
    public class HR_ManagerPanel
    {
        private static Window _window = Program.win;
        private static Point origin = new(Program.WIDTH / 2, Program.HEIGHT / 2);
        private static User curUser = Program.currrentUser;
        private static List<User> users = Program.users;
        static int sel = 0;
        private static string alfabeth = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        private static string specsimb = @"!@#$%^&*()_+=\[{\]};:<>|./?,-";
        private static int element_choosen;
        
        
        
        public static void main()
        {
            drawhead();
        }
        public static void drawhead()
        {

            for (int i=0;i<Console.WindowWidth;i++)
            {
                _window.drawDot(new(i,1),'═');
            }
            //for (int i = 0; i < 3; i++)
            //{
            //    if (i == element_choosen)
            //        _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + strings[i]);
            //    else
            //        _window.drawString(new Point(origin.x - 10, origin.y + i), strings[i]);
            //}
            _window.drawString(new (Program.WIDTH/2-20,0),$"HR-Менеджер. Текущий пользователь: {curUser.Name}");
            _window.drawBuffer();
        }

        public static void draw()
        {
            _window.clearBuffer();
            for (int i = 0; i < 4; i++)
            {
                if (i == sel)
                    _window.drawString(new Point(origin.x - 13, origin.y + i), ">> " + i);
                //else
                    //_window.drawString(new Point(origin.x - 10, origin.y + i));
            }
        }

        public static void controls()
        {
            bool end = false;
            var key = Console.ReadKey().Key;
            
            _window.clearBuffer();
            while (!end)
            {
                switch (key)
                {
                    case ConsoleKey.Tab:
                        curUser = null;
                        break;
                    case ConsoleKey.Delete:
                        
                        break;
                    case ConsoleKey.UpArrow: 
                        sel--;
                        break;
                    case ConsoleKey.DownArrow:
                        sel++;
                        break;
                    case ConsoleKey.Enter:

                        break;
                    case ConsoleKey.Escape:
                        break;
                    default:
                        _window.drawString(new Point(0,Program.HEIGHT-1),"Something went wrong; are you fuzzing this program?",ConsoleColor.White,ConsoleColor.Red);
                        break;
                }
            }
        }

        public static string loginGenerate()
        {
            Random rnd = new Random();
            string loginresult = "";
            for (int i = 0; i < 15; i++)
            {
                loginresult += alfabeth[rnd.Next(alfabeth.Length)];
            }
            return loginresult;
        }
        public static string passwordGenerate()
        {
            Random rnd = new Random();
            string passresult = "";
            for (int i = 0; i < 3; i++)
            {
                passresult += alfabeth[rnd.Next(alfabeth.Length)];
            }

            for (int i = 0; i < 3; i++)
            {
                passresult += Convert.ToString(rnd.Next(0, 9));
            }

            for (int i = 0; i < 2; i++)
            {
                passresult += specsimb[rnd.Next(specsimb.Length)];
            }
            return passresult;
        }

        public static void newworker()
        {
            bool err = true;
            loginGenerate();
            passwordGenerate();
            var FIO = Console.ReadLine();
            var birthdate = Console.ReadLine();
            //if (err)
            //{
            //    Program.users.Add(new User(strs[0], Convert.ToDateTime(strs[5]), Role.CLIENT, strs[1],
            //        strs[2], strs[3], "+7" + strs[4]));
            //    string outer = JsonConvert.SerializeObject(Program.users);
            //    File.WriteAllText("users.json", outer);
            //}
        }

        public static void delworker()
        {
            
        }

        public static void changeworkerpost()
        {
            
        }
    }
    
}