using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment8 
{
    //This is the definition of user defined class Article
    class Article : IComparable
    {
        public static bool priceCompare = false;
        public static bool idCompare = false;

        private string title;
        private int id;
        private double price;

        public Article(string title, int id, double price)
        {

            this.title = title;

            this.id = id;
            this.price = price;

        }

        public string Title
        {
            get
            {
                return title;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
        }

        public double Price
        {   
            get
            {
                return price;
            }
        }


        public override string ToString()
        {
            return title + " " + id;

            //return base.ToString();
        }


        public int CompareTo(object obj)
        {
            if (priceCompare)
                return this.price.CompareTo(((Article)obj).price);
            else if (idCompare)
                return this.id.CompareTo(((Article)obj).id);
            else
                return this.title.CompareTo(((Article)obj).title);
        }
    }
}
