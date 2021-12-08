using System;

namespace Practice_9.Classes.Panels
{
    public class Item
    {
        public string name;
        public int price;
        public int count;
        public Category category;
        public DateTime exp_date;
        public bool Expired = false;
        

        public Item(string name, int price, int count, Category category,DateTime expDate)
        {
            this.name = name;
            this.price = price;
            this.count = count;
            this.category = category;
            exp_date = expDate;
            if (expDate < DateTime.Today)
            {
                Expired = true;
            }
        }

    }
}