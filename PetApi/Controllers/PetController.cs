using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetApi.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ControllerBase
    {
        public static List<Pet> Pets { get; set; } = new List<Pet>();

        [HttpPost]
        public Pet PostPet(Pet pet)
        {
            Pets.Add(pet);
            return pet;
        }

        [HttpGet]
        public List<Pet> GetAllPets()
        {
            return Pets;
        }

        [HttpGet("{name}")]
        public Pet GetPetByName(string name)
        {
            return Pets.FirstOrDefault(pet => name.Equals(pet.Name));
        }

        [HttpDelete("{name}")]
        public void DeletePetByName(string name)
        {
            Pets.RemoveAll(pet => name.Equals(pet.Name));
        }

        [HttpPut("{name}")]
        public Pet UpdatePetPrice(string name, Pet petUpdate)
        {
            var petFound = Pets.Find(pet => pet.Name.Equals(name));
            petFound.Price = petUpdate.Price;
            return petFound;
        }
    }
}
