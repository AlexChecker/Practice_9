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
        private static List<User> users = Program.users;
        static int sel = 0;
        private static string alfabeth = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        private static string specsimb = @"!@#$%^&*()_+=\[{\]};:<>|./?,-";
        private static int element_choosen=0;
        
        
        
        public static void main()
        {
            if (!WorkerSetup.main()) return;
            _window.clearBuffer();
            drawhead();
        }

        private static void mainmenu()
        {
            
        }

        public static void drawhead()
        {

            for (int i=0;i<Console.WindowWidth;i++)
            {
                _window.drawDot(new(i,1),'═');
            }
            _window.drawString(new (Program.WIDTH/2-20,0),$"HR-Менеджер. Текущий пользователь: {Program.currrentUser.Name}");
            _window.drawBuffer();
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
            //Program.users.Add(new User());
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