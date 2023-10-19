using Microsoft.AspNetCore.Mvc;
using PetApi.Models;
using System.Collections.Generic;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private static List<Pet> pets = new List<Pet>();

        [HttpPost]
        public Pet Add(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }

        [HttpGet]
        public List<Pet> Get()
        {
            return pets;
        }

        [HttpGet("{name}")]
        public Pet Find(string name)
        {
            return pets.Find(pet => pet.Name == name);
        }

        [HttpDelete("{name}")]
        public void Remove(string name)
        {
            pets.Remove(pets.Find(pet => pet.Name == name));
        }

        [HttpPut("{name}")]
        public Pet UpdatePrice(string name, int price)
        {
            Pet petToUpdate = pets.Find(pet => pet.Name == name);
            petToUpdate.Price = price;
            return petToUpdate;
        }

        [HttpGet("type")]
        public List<Pet> FindByType(string type)
        {
            return pets.FindAll(pet => pet.Type == type);
        }

        [HttpGet("price")]
        public List<Pet> FindByPrice(int minPrice, int maxPrice)
        {
            return pets.FindAll(pet => pet.Price >= minPrice && pet.Price <= maxPrice);
        }

        [HttpGet("color")]
        public List<Pet> FindByColor(string color)
        {
            return pets.FindAll(pet => pet.Color == color);
        }

        [NonAction]
        public void Reset()
        {
            pets.Clear();
        }
    }
}
