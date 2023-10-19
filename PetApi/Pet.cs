using System;

namespace PetApi
{
    public class Pet
    {
        public Pet()
        {
        }

        public Pet(string name, PetType type, string color, float price)
        {
            Name = name;
            Type = type;
            Color = color;
            Price = price;
        }

        public string Name { get; set; }
        public PetType Type { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Pet pet &&
                   Name == pet.Name &&
                   Type == pet.Type &&
                   Color == pet.Color &&
                   Price == pet.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Color, Price);
        }
    }
}