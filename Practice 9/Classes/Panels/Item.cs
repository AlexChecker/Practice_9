using System;
using System.Collections.Generic;

namespace Practice_9.Classes.Panels
{
    public class Item
    {
        public string name;
        public int price;
        public int count;
        public Category category;


        public Item(string name, int price, int count, Category category)
        {
            this.name = name;
            this.price = price;
            this.count = count;
            this.category = category;
        }

        public Item ICloneable(int countn = 1)
        {
            return new Item(name,price,countn,category);
        }
    }
}