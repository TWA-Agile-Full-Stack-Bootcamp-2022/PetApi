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
        public Pet GetByName(string name)
        {
            return Pets.Find(pet => pet.Name.Equals(name));
        }

        [HttpDelete("{name}")]
        public void Sell(string name)
        {
        }

        [HttpDelete]
        public void Del()
        {
            Pets = new List<Pet>();
        }
    }
}