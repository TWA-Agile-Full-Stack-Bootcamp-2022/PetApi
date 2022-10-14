using System;
using System.Drawing;

namespace PetApi
{
    public class Pet
    {
        public Pet()
        {
        }

        public Pet(string name, string type, string color, double price)
        {
            this.Name = name;
            this.Type = type;
            this.Color = color;
            this.Price = price;
        }
        
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
    }
}