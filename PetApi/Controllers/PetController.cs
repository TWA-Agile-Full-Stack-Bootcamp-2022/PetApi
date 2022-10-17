using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PetApi
{
    [ApiController]
    [Route("api")]
    public class PetController : Controller
    {
        private static List<Pet> pets = new List<Pet>();

        [HttpPost("pets")]
        public Pet AddPet(Pet pet)
        {
            pets.Add(pet);
            return pet;
        }

        [HttpGet("pets")]
        public List<Pet> GetAllPets()
        {
            return pets;
        }

        [HttpGet("pets/{name}")]
        public Pet GetAllPets(string name)
        {
            return pets.FirstOrDefault(pet => pet.Name.Equals(name));
        }

        [HttpDelete("pets")]
        public void DeleteAllPets()
        {
            pets.Clear();
        }
    }
}