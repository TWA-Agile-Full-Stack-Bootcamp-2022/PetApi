namespace PetApi.Models
{
    public class Pet
    {
        public Pet(string name, string type, string color, int price)
        {
            this.Name = name;
            this.Type = type;
            this.Color = color;
            this.Price = price;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Pet);
        }

        protected bool Equals(Pet other)
        {
           return Name == other.Name && Type == other.Type && Color == other.Color && Price == other.Price;
        }
    }
}
