using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Practice_9.Classes.Panels
{
    public static class Warehouse
    {
        public static List<Item> _items = new List<Item>();
        public static List<Item> ItempooList = new List<Item>();

        public static void init(string filename)
        {
            string itempool = File.ReadAllText(@$"itempools\{filename}");
            ItempooList = JsonConvert.DeserializeObject<List<Item>>(itempool);
            //foreach (string item in File.ReadLines(@$"itempools\{filename}"))
            //{
            //    //Формат: Имя|цена|кол-во|категория|дата
            //    string[] positions = item.Split('|');
            //    switch (Convert.ToInt32(positions[3]))
            //    {
            //        case 0:
            //            ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.JACKETS));
            //            break;
            //        case 1:
            //            ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.ACCESSOIRES));
            //            break;
            //        case 2:
            //            ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.ACCESSORIES_FIT));
            //            break;
            //        case 3:
            //            ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.BOOTS));
            //            break;
            //        case 4:
            //            ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.SNEAKERS));
            //            break;
            //        case 5:
            //            ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.HOODIES));
            //            break;
            //        default:
            //            ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.OTHER));
            //            break;
            //    }
            //}
        }

        public static void newItem(string name,int price, int count, Category category)
        {
            ItempooList.Add(new (name,price,count,category));
        }

        public static void newItem(string name, int price, int count)
        {
            ItempooList.Add(new (name,price,count,Category.OTHER));
        }

        public static void addItem(int ID,int amount)
        {
            _items.Add(ItempooList[ID]);
            _items.Last().count = amount;
        }

        public static void delItem(int ID)
        {
            _items.RemoveAt(ID);
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
            List<string> tos = new List<string>();
            string itempool = JsonConvert.SerializeObject(tos);
            //foreach (Item item in ItempooList)
            //{
            //    tos.Add($"{item.name}|{item.price}|{item.count}|{item.category.ID}|{item.exp_date.ToString("d")}");
            //}
            File.WriteAllText("itempools/itempool.json",itempool);
        }

        public static void saveWarehouse()
        {
            List<string> tos = new List<string>();
            string warehouse = JsonConvert.SerializeObject(tos);
            //foreach (Item item in _items)
            //{
            //    tos.Add($"{item.name}|{item.price}|{item.count}|{item.category.ID}|{item.exp_date.ToString("d")}");
            //}
            File.WriteAllText("itempools/warehouse.json",warehouse);
        }

        public static void save()
        {
            saveItemPool();
            saveWarehouse();
        }
    }
}