using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private static List<Pet> Pets { get; set; } = new List<Pet>();
        [HttpPost]
        public ActionResult<Pet> AddPet(Pet pet)
        {
            if (Pets.Any(savedPet => pet.Name.Equals(savedPet.Name)))
            {
                return BadRequest();
            }

            Pets.Add(pet);
            return pet;
        }

        [HttpGet]
        public List<Pet> List()
        {
            return Pets;
        }

        [HttpGet("{name}")]
        public ActionResult<Pet> GetByName(string name)
        {
            var pet = Pets.Find(pet => pet.Name.Equals(name));
            if (pet == null)
            {
                return NotFound();
            }

            return pet;
        }

        [HttpGet("types/{type}")]
        public List<Pet> FindByType(string type)
        {
            return Pets.Where(pet => pet.Type.Equals(type)).ToList();
        }

        [HttpPut("{name}")]
        public ActionResult<Pet> UpdatePrice(string name, Pet pet)
        {
            var petSearch = Pets.Find(pet => pet.Name.Equals(name));
            if (petSearch == null)
            {
                return NotFound();
            }

            petSearch.Price = pet.Price;
            return petSearch;
        }

        [HttpDelete("{name}")]
        public void Sell(string name)
        {
            var soldPet = Pets.First(pet => pet.Name.Equals(name));
            Pets.Remove(soldPet);
        }

        [HttpDelete]
        public void Del()
        {
            Pets = new List<Pet>();
        }
    }
}