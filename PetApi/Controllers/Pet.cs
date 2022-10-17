using System;

namespace PetApi
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

        public int Price { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var pet = obj as Pet;
            return pet != null && Equals(pet);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Color, Price);
        }

        private bool Equals(Pet pet)
        {
            return Name.Equals(pet.Name) &&
                   Type.Equals(pet.Type) &&
                   Color.Equals(pet.Color) &&
                   Price.Equals(pet.Price);
        }
    }
}