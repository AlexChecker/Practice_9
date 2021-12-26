using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{
    public class HR_ManagerPanel
    {
        private static readonly Window _window = Program.win;
        private static Point origin = new(Program.WIDTH / 2, Program.HEIGHT / 2);
        private static readonly List<User> users = Program.users;
        private static int sel;
        private static readonly string alfabeth = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        private static readonly string specsimb = @"!@#$%^&*()_+=\[{\]};:<>|./?,-";


        public static void main()
        {
            if (Program.currrentUser.Name == null)
                if (!WorkerSetup.main())
                    return;
            _window.clearBuffer();
            drawhead();
            mainmenu();
        }

        private static void drawuser(List<string> us, int sel)
        {
            var m = new List<string>();
            m.Add($"Name: {us[0]}");
            m.Add($"E-mail: {us[1]}");
            m.Add($"Phone: +7 {us[2]}");
            m.Add($"Birthday: {us[3]}");
            m.Add($"Register Date: {us[4]}");
            for (var i = 0; i < m.Count; i++)
                if (i == sel) _window.drawString(new Point(0, i + 3), $"[{m[i]}]");
                else _window.drawString(new Point(0, i + 3), $"{m[i]}");
            m.Clear();
            _window.drawBuffer();
        }

        private static void edit(User user)
        {
            var strings = new List<string>();
            strings.Add("");
            strings.Add("");
            strings.Add("");
            strings.Add("");
            strings.Add("");
            var sel = 0;
            var end = false;
            bool err = false;
            while (!end)
            {
                err = false;
                _window.clearBuffer();
                drawhead();
                drawuser(strings, sel);
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (sel == 0) sel = strings.Count - 1;
                        else sel--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (sel == strings.Count - 1) sel = 0;
                        else sel++;
                        break;
                    case ConsoleKey.Enter:
                        foreach (var us in Program.users.ToArray())
                            if (us.login == strings[1] || us.email == strings[3] || us.phone == strings[4])
                                err = true;

                        try
                        {
                            Convert.ToDateTime(strings[3]);
                            Convert.ToDateTime(strings[4]);
                        }
                        catch (Exception e)
                        {
                            err = true;
                        }

                        if (!err)
                        {
                            user.Name = strings[0];
                            user.email = strings[1];
                            user.phone = "+7"+strings[2].Replace("/","");
                            user.Birthday = Convert.ToDateTime(strings[3]);
                            user.regDate = Convert.ToDateTime(strings[4]);
                            end = true;
                        }
                        break;
                    case ConsoleKey.Escape:
                        end = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (strings[sel].Length != 0) strings[sel] = strings[sel].Remove(strings[sel].Length - 1);
                        break;
                    default:
                        var ch = ' ';
                        ch = key.KeyChar;
                        Console.Write(ch);
                        
                        if (sel == 0 && strings[0].Length < 30) strings[0] += ch;

                        if (sel == 1 && strings[1].Length < 30) strings[1] += ch;

                        if (sel == 2 && strings[2].Length < 13 && char.IsDigit(ch))
                        {
                            if (strings[2].Length == 3 || strings[2].Length == 7||strings[2].Length==10)
                                strings[2] += "/" + ch;
                            else
                                strings[2] += ch;
                        }

                        if (sel == 3 && strings[3].Length <= 9)
                        {
                            if (strings[3].Length == 2 || strings[3].Length == 5)
                                strings[3] += "/" + ch;
                            else
                                strings[3] += ch;
                        }

                        if (sel == 4 && strings[4].Length <= 9)
                        {
                            if (strings[4].Length == 2 || strings[4].Length == 5)
                                strings[4] += "/" + ch;
                            else
                                strings[4] += ch;
                        }

                        break;
                }
            }
        }

        private static void menus()
        {
            _window.drawString(new Point(0, 3), "Name");
            _window.drawString(new Point(15, 3), "Role");
            _window.drawString(new Point(30, 3), "E-mail");
            _window.drawString(new Point(45, 3), "Login");
            _window.drawString(new Point(60, 3), "Phone");
            _window.drawString(new Point(75, 3), "Birthday");
            _window.drawString(new Point(94, 3), "Registration date");
            _window.drawString(new Point(120,3),"Password");
            for (var i = 0; i < users.Count; i++)
                if (i == sel)
                {
                    _window.drawString(new Point(0, i + 4), users[i].Name, ConsoleColor.Cyan);
                    _window.drawString(new Point(15, i + 4), users[i].role.name, ConsoleColor.Cyan);
                    _window.drawString(new Point(30, i + 4), users[i].email, ConsoleColor.Cyan);
                    _window.drawString(new Point(45, i + 4), users[i].login, ConsoleColor.Cyan);
                    _window.drawString(new Point(60, i + 4), users[i].phone, ConsoleColor.Cyan);
                    _window.drawString(new Point(75, i + 4), users[i].Birthday.Date.ToString(), ConsoleColor.Cyan);
                    _window.drawString(new Point(94, i + 4), users[i].regDate.Date.ToString(), ConsoleColor.Cyan);
                    _window.drawString(new Point(120,i+4),users[i].password,ConsoleColor.Cyan);
                }
                else
                {
                    _window.drawString(new Point(0, i + 4), users[i].Name);
                    _window.drawString(new Point(15, i + 4), users[i].role.name);
                    _window.drawString(new Point(30, i + 4), users[i].email);
                    _window.drawString(new Point(45, i + 4), users[i].login);
                    _window.drawString(new Point(60, i + 4), users[i].phone);
                    _window.drawString(new Point(75, i + 4), users[i].Birthday.ToString());
                    _window.drawString(new Point(94, i + 4), users[i].regDate.ToString());
                    _window.drawString(new Point(120,i+4),users[i].password);
                }

            _window.drawBuffer();
        }

        private static void mainmenu()
        {
            var end = false;
            while (!end)
            {
                if (Program.currrentUser == null) break;

                _window.clearBuffer();
                drawhead();
                menus();
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (sel == 0) sel = users.Count - 1;
                        else sel--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (sel == users.Count - 1) sel = 0;
                        else sel++;
                        break;
                    case ConsoleKey.Enter:
                        edit(users[sel]);
                        break;
                    case ConsoleKey.N:
                        Program.users.Add(new User(null,DateTime.Now, selectRole(),loginGenerate(),passwordGenerate(),null,null));
                        break;
                    case ConsoleKey.S:
                        var tos = JsonConvert.SerializeObject(Program.users);
                        File.WriteAllText("users.json", tos);
                        break;
                    case ConsoleKey.Delete:
                        Program.users.Remove(Program.users[sel]);
                        break;
                    case ConsoleKey.Escape:
                        Program.currrentUser = null;
                        break;
                }
            }
        }


        private static void drawRoles(List<Role> roles,int sel)
        {
            
            for (int i = 0; i < roles.Count; i++)
            {
                if (i == sel)
                {
                    _window.drawString(new Point(0,i+3),$">> {roles[i].name}");
                }
                else _window.drawString(new Point(0,i+3),$"   {roles[i].name}");
            }
            _window.drawBuffer();
        }

        private static Role selectRole()
        {List<Role> roles = new List<Role>();
            roles.Add(Role.ADMIN);
            roles.Add(Role.HR);
            roles.Add(Role.OPERATOR);
            roles.Add(Role.ACCOUNTANT);
            roles.Add(Role.MANAGER);
            roles.Add(Role.CLIENT);
            Role result = null;
            int selected = 0;
            bool exit = false;
            while (!exit)
            {
                _window.clearBuffer();
                drawhead();
                drawRoles(roles,selected);
                
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected == 0) selected = 5;
                        else selected--;
                        break;
                    
                    case ConsoleKey.DownArrow:
                        if (selected == 5) selected = 0;
                        else selected++;
                        break;
                    
                    case ConsoleKey.Enter:
                        return roles[selected];
                        break;
                }
            }
            return result;
        }

        public static void drawhead()
        {
            for (var i = 0; i < Console.WindowWidth; i++) _window.drawDot(new Point(i, 1), '═');
            _window.drawString(new Point(Program.WIDTH / 2 - 20, 0),
                $"HR-Менеджер. Текущий пользователь: {Program.currrentUser.Name}");
            _window.drawBuffer();
        }

        public static string loginGenerate()
        {
            var rnd = new Random();
            var loginresult = "";
            for (var i = 0; i < 10; i++) loginresult += alfabeth[rnd.Next(alfabeth.Length)];
            return loginresult;
        }

        public static string passwordGenerate()
        {
            var rnd = new Random();
            var passresult = "";
            for (var i = 0; i < 4; i++) passresult += alfabeth[rnd.Next(alfabeth.Length)];

            for (var i = 0; i < 4; i++) passresult += Convert.ToString(rnd.Next(0, 9));

            for (var i = 0; i < 4; i++) passresult += specsimb[rnd.Next(specsimb.Length)];
            return passresult;
        }



        public static void delworker()
        {
        }

        public static void changeworkerpost()
        {
        }
    }
}