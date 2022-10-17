﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
