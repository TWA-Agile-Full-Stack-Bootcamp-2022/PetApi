using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PetApi
{
    [ApiController]
    [Route("api/pets")]
    public class PetController : Controller
    {
        private static List<Pet> pets = new List<Pet>();

        [HttpPost]
        public Pet AddPet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }

        [HttpGet]
        public List<Pet> GetAllPets([FromQuery] string? type, [FromQuery] int? priceFrom, [FromQuery] int? priceTo,
            [FromQuery] string? color)
        {
            if (type != null)
            {
                return pets.FindAll(pet => pet.Type.Equals(type));
            }

            if (priceFrom != null && priceTo != null)
            {
                return pets.FindAll(pet => pet.Price >= priceFrom && pet.Price <= priceTo);
            }

            if (color != null)
            {
                return pets.FindAll(pet => pet.Color.Equals(color));
            }

            return pets;
        }

        [HttpGet("{name}")]
        public Pet GetPetByName(string name)
        {
            return pets.FirstOrDefault(pet => pet.Name.Equals(name));
        }

        [HttpDelete]
        public void DeleteAllPets()
        {
            pets.Clear();
        }

        [HttpDelete("{name}")]
        public void DeletePetsByName(string name)
        {
            var removedCount = pets.RemoveAll(pets => pets.Name.Equals(name));
            Console.WriteLine("delete pets by name {0}, deletedCount={1}", name, removedCount);
        }

        [HttpPut("{name}")]
        public ActionResult<Pet> ModifyPetsBy(string name, Pet pet)
        {
            var petToModify = pets.FirstOrDefault(pet => pet.Name.Equals(name));
            if (petToModify == null)
            {
                return NotFound();
            }

            petToModify = pet;
            return Ok(petToModify);
        }
    }
}