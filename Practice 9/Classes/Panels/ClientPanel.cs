using System;
using System.Collections.Generic;
using Practice_9.Drawing;

namespace Practice_9.Classes.Panels
{
    /// <summary>
    ///     Панель клиента
    /// </summary>
    public static class ClientPanel
    {
        private static readonly Window win = Program.win;
        private static readonly User client = Program.currrentUser;
        private static readonly List<Item> cart = new();

        private static void drawhead()
        {
            win.drawString(new Point(Program.WIDTH / 2 - 15, 0), $"Текущий пользователь: {client.Name}");
            for (var i = 0; i < Program.WIDTH; i++) win.drawString(new Point(i, 1), "═");
            win.drawBuffer();
        }

        private static Category catSelector()
        {
            //Category result = new Category(6);
            var categories = new List<Category>();
            categories.Add(Category.JACKETS);
            categories.Add(Category.ACCESSOIRES);
            categories.Add(Category.ACCESSORIES_FIT);
            categories.Add(Category.BOOTS);
            categories.Add(Category.SNEAKERS);
            categories.Add(Category.HOODIES);
            categories.Add(Category.OTHER);
            var end = false;
            var sel = 0;
            win.clearBuffer();
            drawhead();
            while (!end)
            {
                for (var i = 0; i < categories.Count; i++)
                    if (i == sel) win.drawString(new Point(0, i + 3), $"[{categories[i].name}]");
                    else win.drawString(new Point(0, i + 3), $"  {categories[i].name} ");

                var key = Console.ReadKey().Key;
                win.clearBuffer();
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (sel == 0) sel = categories.Count - 1;
                        else sel--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (sel == categories.Count - 1) sel = 0;
                        else sel++;
                        break;
                    case ConsoleKey.Enter:

                        return categories[sel];
                        break;
                }
            }

            return Category.OTHER;
        }

        private static void drawItems(int sel, List<Item> list)
        {
        }

        private static void controls(List<Item> categorized)
        {
            var end = false;
            var sel = 0;
            while (!end)
            {
                foreach (var item in categorized)
                    if (item.count <= 0)
                    {
                        categorized.Remove(item);
                        Warehouse._items.Remove(item);
                    }

                var key = Console.ReadKey().Key;
                win.clearBuffer();
                drawhead();
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (sel == 0) sel = categorized.Count - 1;
                        else sel--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (sel == categorized.Count - 1) sel = 0;
                        else sel++;
                        break;
                    case ConsoleKey.Enter:
                        //fkdfkdshfjkhdsjkfhjkdshfjkhdskjfhjkdshfjk


                        cart.Add(categorized[sel]);
                        break;
                    case ConsoleKey.Escape:
                        end = true;
                        break;
                }
            }
        }

        public static void main()
        {
            var end = false;
            var category = catSelector();
            var categorized = new List<Item>();
            while (!end)
            {
                drawhead();
                if (category == null)
                {
                    win.drawString(new Point(0, 3), "Do you want to select products?");
                    var key = Console.ReadKey();
                    if (key.KeyChar == 'y')
                    {
                        categorized.Clear();
                        category = catSelector();
                    }
                }

                foreach (var item in Warehouse._items)
                    if (item.category == category)
                        categorized.Add(item);
                controls(categorized);
            }
        }
    }
}