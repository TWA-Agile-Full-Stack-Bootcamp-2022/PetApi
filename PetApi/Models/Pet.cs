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
    }
}
