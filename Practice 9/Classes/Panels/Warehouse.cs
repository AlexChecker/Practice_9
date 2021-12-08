using System;
using System.Collections.Generic;
using System.IO;

namespace Practice_9.Classes.Panels
{
    public static class Warehouse
    {
        public static List<Item> _items = new List<Item>();
        public static List<Item> ItempooList = new List<Item>();

        public static void init(string filename)
        {
            foreach (string item in File.ReadLines(filename))
            {
                //Формат: Имя|цена|кол-во|категория|дата
                string[] positions = item.Split('|');
                switch (Convert.ToInt32(positions[3]))
                {
                    case 0:
                        ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.JACKETS,Convert.ToDateTime(positions[4])));
                        break;
                    case 1:
                        ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.ACCESSOIRES,Convert.ToDateTime(positions[4])));
                        break;
                    case 2:
                        ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.ACCESSORIES_FIT,Convert.ToDateTime(positions[4])));
                        break;
                    case 3:
                        ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.BOOTS,Convert.ToDateTime(positions[4])));
                        break;
                    case 4:
                        ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.SNEAKERS,Convert.ToDateTime(positions[4])));
                        break;
                    case 5:
                        ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.HOODIES,Convert.ToDateTime(positions[4])));
                        break;
                    default:
                        ItempooList.Add(new (positions[0],Convert.ToInt32(positions[1]), Convert.ToInt32(positions[2]),Category.OTHER,Convert.ToDateTime(positions[4])));
                        break;
                }
            }
        }

        public static void newItem(string name,int price, int count, Category category, DateTime expdate)
        {
            ItempooList.Add(new (name,price,count,category,expdate));
        }

        public static void newItem(string name, int price, int count, DateTime expdate)
        {
            ItempooList.Add(new (name,price,count,Category.OTHER,expdate));
        }

        public static void addItem(int ID)
        {
            _items.Add(ItempooList[ID]);
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

        public static void saveItemPool(string filename)
        {
            List<string> tos = new List<string>();
            foreach (Item item in ItempooList)
            {
                tos.Add($"{item.name}|{item.price}|{item.count}|{item.category.ID}|{item.exp_date.ToString("d")}");
            }
            File.WriteAllLines(filename,tos);
        }

        public static void saveWarehouse(string filename)
        {
            List<string> tos = new List<string>();
            foreach (Item item in _items)
            {
                tos.Add($"{item.name}|{item.price}|{item.count}|{item.category.ID}|{item.exp_date.ToString("d")}");
            }
            File.WriteAllLines(filename,tos);
        }

        public static void save(string poolname,string warehousename)
        {
            saveItemPool(poolname);
            saveWarehouse(warehousename);
        }
    }
}