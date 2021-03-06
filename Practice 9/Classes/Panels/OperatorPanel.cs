using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{
    public static class OperatorPanel
    {
        private static readonly Window _window = Program.win;
        private static int hilighted = 0;

        public static void main()
        {
            if (Program.currrentUser.Name == null)
                if (!WorkerSetup.main())
                    return;
            _window.clearBuffer();
            drawhead();
            drawWarehouse();
        }

        /// <summary>
        ///     Это параша для отрисовки хуйни с хоткеями
        /// </summary>
        /// <param name="warehouse">в складе или только добавляем в него?</param>
        private static void hotkeys(bool warehouse)
        {
            if (warehouse)
            {
                string[] hotkeys =
                    {"A: Добавить товары", "DEL: удалить товар", "С: изменить кол-во", "S: сохранить", "ESC: выйти"};
                //string[] hotkeys = { "C: Создать товар", "DEL: удалить товар", "TAB: выйти", "S: сохранить", "ESC: вернуться в склад","ENTER: добавить в склад" };
                var offset = Program.WIDTH / 5;
                for (var i = 0; i < 5; i++)
                    _window.drawString(new Point(i * offset, Program.HEIGHT - 1), hotkeys[i], ConsoleColor.DarkGray,
                        ConsoleColor.Cyan);
            }
            else
            {
                string[] hotkeys =
                {
                    "C: Создать товар", "DEL: удалить товар", "TAB: выйти", "S: сохранить", "ESC: вернуться в склад",
                    "ENTER: добавить в склад"
                };

                var offset = Program.WIDTH / 6;
                for (var i = 0; i < 6; i++)
                    _window.drawString(new Point(i * offset, Program.HEIGHT - 1), hotkeys[i], ConsoleColor.DarkGray,
                        ConsoleColor.Cyan);
            }

            _window.drawBuffer();
        }

        public static void drawhead()
        {
            for (var i = 0; i < Console.WindowWidth; i++) _window.drawDot(new Point(i, 1), '═');
            _window.drawString(new Point(Program.WIDTH / 2 - 20, 0),
                $"Оператор склада. Текущий пользователь: {Program.currrentUser.Name}");
            _window.drawBuffer();
        }

        public static void createItem()
        {
            _window.clearBuffer();
            _window.drawString(new Point(0, 0), "Введите название товара: ");
            _window.drawBuffer();
            Console.SetCursorPosition(0, 1);
            var name = Console.ReadLine();
            Price_go:
            _window.clearBuffer();
            _window.drawString(new Point(0, 0), "Введите цену товара: ");
            _window.drawBuffer();
            var price = 0;
            Console.SetCursorPosition(0, 1);
            try
            {
                price = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Попробуйте снова");
                Thread.Sleep(700);
                goto Price_go;
            }

            var categories = new List<string>();
            categories.Add("Jackets");
            categories.Add("Accessoires");
            categories.Add("Fitness accessoires");
            categories.Add("Boots");
            categories.Add("Sneakers");
            categories.Add("Hoodies");
            categories.Add("Other");

            var sel = 0;
            var end = false;
            while (!end)
            {
                _window.clearBuffer();
                for (var i = 0; i < categories.Count; i++)
                    if (i == sel) _window.drawString(new Point(0, i + 2), $"[{categories[i]}]");
                    else _window.drawString(new Point(0, i + 2), $" {categories[i]}");
                _window.drawBuffer();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (sel == 0) sel = categories.Count - 1;
                        else sel--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (sel == categories.Count) sel = 0;
                        else sel++;
                        break;
                    case ConsoleKey.Enter:
                        Warehouse.newItem(name, price, 1, new Category(sel));
                        end = true;
                        break;
                }
            }

            _window.clearBuffer();
        }

        //Параша для отрисовки того, что может быть в складе
        public static void drawItemPool()
        {
            if (Warehouse.ItempooList.Count == 0)
            {
                if (File.Exists("itempools/itempool.json"))
                    Warehouse.init("itempool.json");
                else createItem();
            }

            _window.clearBuffer();
            drawhead();
            var end = false;
            var sel = 0;
            for (var i = 0; i < Warehouse.ItempooList.Count; i++)
            {
                if (i == sel)
                {
                    _window.drawString(new Point(0, i + 3), Warehouse.ItempooList[i].name, ConsoleColor.Cyan);
                    _window.drawString(new Point(20, i + 3), Warehouse.ItempooList[i].price.ToString(),
                        ConsoleColor.Cyan);
                    //_window.drawString(new Point(26, i + 3), Warehouse.ItempooList[i].count.ToString(),ConsoleColor.Cyan);
                    _window.drawString(new Point(31, i + 3), Warehouse.ItempooList[i].category.name, ConsoleColor.Cyan);
                }
                else
                {
                    _window.drawString(new Point(0, i + 3), Warehouse.ItempooList[i].name);
                    _window.drawString(new Point(20, i + 3), Warehouse.ItempooList[i].price.ToString());
                    //_window.drawString(new Point(26, i + 3), Warehouse.ItempooList[i].count.ToString());
                    _window.drawString(new Point(31, i + 3), Warehouse.ItempooList[i].category.name);
                }

                _window.drawBuffer();
            }

            while (!end)
            {
                if (Program.currrentUser == null)
                    break;
                drawhead();
                hotkeys(false);
                var key = Console.ReadKey().Key;
                _window.clearBuffer();
                switch (key)
                {
                    case ConsoleKey.C:
                        createItem();
                        break;
                    case ConsoleKey.S:
                        Warehouse.save();
                        break;
                    case ConsoleKey.Tab:
                        Program.currrentUser = null;
                        end = true;
                        break;
                    case ConsoleKey.Delete:
                        try
                        {
                            Warehouse.delItemfromPool(sel);
                        }
                        catch (Exception)
                        {
                        }

                        break;
                    case ConsoleKey.UpArrow:
                        if (sel == 0) sel = Warehouse.ItempooList.Count - 1;
                        else sel--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (sel == Warehouse.ItempooList.Count - 1) sel = 0;
                        else sel++;
                        break;
                    case ConsoleKey.Enter:
                        _window.drawString(new Point(0, Program.HEIGHT - 3), "Please, set amount of items to add:");
                        _window.drawBuffer();
                        Console.SetCursorPosition(0, Program.HEIGHT - 2);
                        try
                        {
                            var a = Convert.ToInt32(Console.ReadLine());
                            Warehouse.addItem(sel, a);
                        }
                        catch (Exception e)
                        {
                            goto default;
                        }

                        break;
                    case ConsoleKey.Escape:
                        if (Warehouse._items.Count != 0) end = true;
                        else
                            _window.drawString(new Point(0, Program.HEIGHT - 1),
                                "There is no elements in warehouse, nothing to open", ConsoleColor.White,
                                ConsoleColor.Red);
                        break;
                    default:
                        _window.drawString(new Point(0, Program.HEIGHT - 1),
                            "Something went wrong; are you fuzzing this program?", ConsoleColor.White,
                            ConsoleColor.Red);
                        break;
                }

                for (var i = 0; i < Warehouse.ItempooList.Count; i++)
                {
                    if (i == sel)
                    {
                        _window.drawString(new Point(0, i + 3), Warehouse.ItempooList[i].name, ConsoleColor.Cyan);
                        _window.drawString(new Point(20, i + 3), Warehouse.ItempooList[i].price.ToString(),
                            ConsoleColor.Cyan);
                        //_window.drawString(new Point(26, i + 3), Warehouse.ItempooList[i].count.ToString(),ConsoleColor.Cyan);
                        _window.drawString(new Point(31, i + 3), Warehouse.ItempooList[i].category.name,
                            ConsoleColor.Cyan);
                    }
                    else
                    {
                        _window.drawString(new Point(0, i + 3), Warehouse.ItempooList[i].name);
                        _window.drawString(new Point(20, i + 3), Warehouse.ItempooList[i].price.ToString());
                        //_window.drawString(new Point(26, i + 3), Warehouse.ItempooList[i].count.ToString());
                        _window.drawString(new Point(31, i + 3), Warehouse.ItempooList[i].category.name);
                    }

                    _window.drawBuffer();
                }
            }
        }

        //Параша для отрисовки того что в складе
        public static void drawWarehouse()
        {
            var sel = 0;
            while (Program.currrentUser != null)
            {
                _window.clearBuffer();
                drawhead();
                hotkeys(true);
                if (Warehouse._items.Count != 0)
                {
                    foreach (var item in Warehouse._items.ToArray())
                        if (item.count <= 0)
                        {
                            Warehouse.delItem(item);
                            Warehouse.save();
                        }

                    for (var i = 0; i < Warehouse._items.Count; i++)
                    {
                        if (i == sel)
                        {
                            _window.drawString(new Point(0, i + 3), Warehouse._items[i].name, ConsoleColor.DarkMagenta,
                                ConsoleColor.DarkCyan);
                            _window.drawString(new Point(20, i + 3), Warehouse._items[i].price.ToString());
                            _window.drawString(new Point(30, i + 3), Warehouse._items[i].count.ToString());
                            _window.drawString(new Point(40, i + 3), Warehouse._items[i].category.name);
                        }
                        else
                        {
                            _window.drawString(new Point(0, i + 3), Warehouse._items[i].name);
                            _window.drawString(new Point(20, i + 3), Warehouse._items[i].price.ToString());
                            _window.drawString(new Point(30, i + 3), Warehouse._items[i].count.ToString());
                            _window.drawString(new Point(40, i + 3), Warehouse._items[i].category.name);
                        }

                        _window.drawBuffer();
                    }


                    var key = Console.ReadKey().Key;
                    _window.clearBuffer();
                    hotkeys(true);
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (sel == 0) sel = Warehouse._items.Count - 1;
                            else sel--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (sel == Warehouse._items.Count - 1) sel = 0;
                            else sel++;
                            break;
                        case ConsoleKey.A:
                            drawItemPool();
                            break;
                        case ConsoleKey.Delete:
                            try
                            {
                                Warehouse.delItem(sel);
                                Warehouse.save();
                            }
                            catch (Exception)
                            {
                            }

                            break;
                        case ConsoleKey.C:
                            _window.drawString(new Point(0, Program.HEIGHT - 4), "Please, enter new amount");
                            _window.drawBuffer();
                            Console.SetCursorPosition(0, Program.HEIGHT - 3);
                            try
                            {
                                var amount = Convert.ToInt32(Console.ReadLine());
                                Warehouse._items[sel].count = amount;
                            }
                            catch (Exception e)
                            {
                                goto default;
                            }

                            break;
                        case ConsoleKey.S:
                            Warehouse.save();
                            break;
                        case ConsoleKey.Escape:
                            Program.currrentUser = null;
                            break;
                        default:
                            _window.drawString(new Point(0, Program.HEIGHT - 1), "Unknown error", ConsoleColor.Red);
                            break;
                    }
                }
                else
                {
                    if (File.Exists("itempools/warehouse.json") &&
                        !(File.ReadAllText("itempools/warehouse.json") == "[]"))
                        Warehouse.loadWarehouse();
                    else
                        drawItemPool();
                }
            }
        }
    }
}