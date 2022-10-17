using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetApi.Models;

namespace PetApi.Controllers
{   
    [ApiController]
    [Route("api")]
    public class PetController : ControllerBase
    {
        private static List<Pet> pets = new List<Pet>();

        [HttpPost("addPet")]
        public Pet AddGet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }
        
        [HttpGet("getAllPets")]
        public List<Pet> GetPets()
        {
            return pets;
        }
        
        [HttpGet("findPetByName")]
        public Pet FindPetByName(string name)
        {
            return pets.First(pet => pet.Name.Equals(name));
        }

        [HttpDelete("removePetByName")]
        public void RemovePetByName(string name)
        {
            var pet = pets.First(pet => pet.Name.Equals(name));
            pets.RemoveAt(pets.IndexOf(pet));
        }

        [HttpPut("modifyPetPrice")]
        public Pet ModifyPetPrice(Pet pet)
        {
            var currentPet = pets.First(pet => pet.Name.Equals(pet.Name));
            currentPet.Price = pet.Price;
            return currentPet;
        }
        
        [HttpGet("findPetsByType")]
        public IEnumerable<Pet> FindPetsByType(string type)
        {
            return pets.Where(pet => pet.Type.Equals(type));
        }
        
        [HttpGet("findPetsByPriceRange")]
        public IEnumerable<Pet> FindPetsByPriceRange(int minPrice, int maxPrice)
        {
            return pets.Where(pet => pet.Price >= minPrice && pet.Price <= maxPrice);
        }

        [HttpGet("findPetsByColor")]
        public IEnumerable<Pet> FindPetsByColor(string color)
        {
            return pets.Where(pet => pet.Color.Equals(color));
        }
        
        [HttpGet("resetPets")]
        public void ResetPets()
        {
            pets.Clear();
        }
    }
}
