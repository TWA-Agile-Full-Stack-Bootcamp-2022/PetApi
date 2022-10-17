using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PetApi.Models
{
    public class Pet
    {
        public Pet()
        {
        }

        public Pet(string name, string type, string color, int price)
        {
            Name = name;
            Type = type;
            Color = color;
            Price = price;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Color, Price);
        }

        public override bool Equals(object obj)
        {
            Pet other = (Pet)obj;
            return Name == other.Name && Type == other.Type && Color == other.Color && Price == other.Price;
        }
    }
}