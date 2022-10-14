using System;
using System.Drawing;

namespace PetApi
{
    public class Pet
    {
        public Pet()
        {
        }

        public Pet(string name, PetType type, Color color, double price)
        {
            this.Name = name;
            this.Type = type;
            this.Color = color;
            this.Price = price;
        }
        
        private string Name { get; set; }
        private PetType Type { get; set; }
        private Color Color { get; set; }
        private double Price { get; set; }
    }
}