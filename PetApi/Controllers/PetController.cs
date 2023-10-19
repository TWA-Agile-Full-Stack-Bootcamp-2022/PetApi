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

        public void Reset()
        {
            pets.Clear();
        }
    }
}
