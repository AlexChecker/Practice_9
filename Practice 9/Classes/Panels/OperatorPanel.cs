using System;
using System.Collections.Generic;
using Practice_9.Drawing;
using System.IO;
using System.Linq;

namespace Practice_9.Classes.Panels
{
    public class OperatorPanel
    {
        private static Window _window = Program.win;
        private User curUser = Program.currrentUser;
        private int hilighted = 0;

        public void main()
        {
            _window.clearBuffer();
            drawhead();
            drawWarehouse();
        }

        public void drawhead()
        {

            for (int i=0;i<Console.WindowWidth;i++)
            {
                _window.drawDot(new(i,1),'═');
            }
            _window.drawString(new (Program.WIDTH/2-7,0),$"Оператор склада. Текущий пользователь: {curUser.Name}");
            _window.drawBuffer();
        }

        public string parse(string way)
        {
            return way.Split('\\').Last();
        }

        public string loadItemPoolFromFile()
        {
            int selected = 0;
            string result = "";
            List<string> files = new List<string>();
            foreach (string s in Directory.GetFiles("itempools"))
            {
                if (parse(s).Split('.').Last() == "list")
                {
                    files.Add(parse(s));
                }
            }
            bool sel = false;
            while (!sel)
            {
                _window.drawString(new (0,2),"Пожалуйста, выберите файл с нужным пулом товаров:");
                drawhead();
                for (int i = 0; i < files.Count; i++)
                {
                    if (i == selected)
                    {
                        _window.drawString(new(0,i+4),$">> {files[i]}");
                    }
                    else
                    {
                        _window.drawString(new Point(3,i+4),files[i]);
                    }
                }
                _window.drawBuffer();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected == 0)
                        {
                            selected = files.Count -1;
                        }
                        else selected--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected == files.Count - 1)
                        {
                            selected = 0;
                        }
                        else
                        {
                            selected++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        result = files[selected];
                        sel = true;
                        break;
                }
                
                _window.clearBuffer();
            }

            return result;
        }

        public void drawItemPool()
        {
            if (Warehouse.ItempooList.Count ==0)
            {
                Warehouse.init(loadItemPoolFromFile());
            }

            for (int i = 0; i < Warehouse.ItempooList.Count; i++)
            {
                if (Warehouse.ItempooList[i].Expired)
                    _window.drawString(new(0,i+3),Warehouse.ItempooList[i].name,ConsoleColor.Red);
                else
                    _window.drawString(new(0,i+3),Warehouse.ItempooList[i].name);
                
                _window.drawString(new (20,i+3),Warehouse.ItempooList[i].price.ToString());
                _window.drawString(new Point(26,i+3),Warehouse.ItempooList[i].count.ToString());
                _window.drawString(new (31,i+3),Warehouse.ItempooList[i].category.name);
                _window.drawBuffer();
            }
            Console.ReadKey();
        }

        public void drawWarehouse()
        {
            int namesize = 0;
            if (Warehouse._items.Count != 0)
            {
                foreach (Item item in Warehouse._items)
                {
                    if (item.name.Length > namesize) namesize = item.name.Length;
                }
            }
            else
            {
                drawItemPool();
            }
        }
    }
}