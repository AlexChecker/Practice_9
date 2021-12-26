using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Practice_9.Classes.Panels
{
    public static class Warehouse
    {
        public static List<Item> _items = new();
        public static List<Item> ItempooList = new();

        public static void init(string filename)
        {
            var itempool = File.ReadAllText(@$"itempools\{filename}");
            ItempooList = JsonConvert.DeserializeObject<List<Item>>(itempool);
        }

        public static void loadWarehouse()
        {
            var warehouse = File.ReadAllText(@"itempools\warehouse.json");
            _items = JsonConvert.DeserializeObject<List<Item>>(warehouse);
        }

        public static void newItem(string name, int price, int count, Category category)
        {
            ItempooList.Add(new Item(name, price, count, category));
        }

        public static void newItem(string name, int price, int count)
        {
            ItempooList.Add(new Item(name, price, count, Category.OTHER));
        }

        public static void addItem(int ID, int amount)
        {
            if (!_items.Contains(ItempooList[ID]))
            {
                _items.Add(ItempooList[ID]);
                _items.Last().count = amount;
            }
        }

        public static void delItem(int ID)
        {
            _items.RemoveAt(ID);
        }

        public static void delItem(Item item)
        {
            _items.Remove(item);
        }

        public static void delItemfromPool(int ID)
        {
            ItempooList.RemoveAt(ID);
        }

        public static void delItemfromPool(Item item)
        {
            ItempooList.Remove(item);
        }

        public static void ClearItemPool()
        {
            ItempooList.Clear();
        }

        public static void saveItemPool()
        {
            //List<string> tos = new List<string>();
            var itempool = JsonConvert.SerializeObject(ItempooList);
            //foreach (Item item in ItempooList)
            //{
            //    tos.Add($"{item.name}|{item.price}|{item.count}|{item.category.ID}|{item.exp_date.ToString("d")}");
            //}
            File.WriteAllText("itempools/itempool.json", itempool);
        }

        public static void saveWarehouse()
        {
            ///List<string> tos = new List<string>();
            var warehouse = JsonConvert.SerializeObject(_items);
            //foreach (Item item in _items)
            //{
            //    tos.Add($"{item.name}|{item.price}|{item.count}|{item.category.ID}|{item.exp_date.ToString("d")}");
            //}
            File.WriteAllText("itempools/warehouse.json", warehouse);
        }

        public static void save()
        {
            saveItemPool();
            saveWarehouse();
        }
    }
}