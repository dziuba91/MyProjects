
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment6_4
{
    [Serializable]
    class Product
    {
        private int ID;
        private string category;
        private string name;
        private float unitPrice;
        private int amount;

        public Product(int ID, string category, string name, float unitPrice, int amount)
        {
            this.ID = ID;
            this.category = category;
            this.name = name;
            this.unitPrice = unitPrice;
            this.amount = amount;
        }

        public int getID()
        {
            return this.ID;
        }

        public override string ToString()
        {
            return 
                "ID : " + ID + "\n" +
                "Category : " + category + "\n" +
                "Name : " + name + "\n" +
                "Unit Price : " + unitPrice + "\n" +
                "Amount : " + amount + "\n";
        }
    }
}
