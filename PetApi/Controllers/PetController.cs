using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace PetApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class PetController : ControllerBase
    {
        private List<Pet> Pets { get; set; } = new List<Pet>();
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult<Pet> AddPet(Pet pet)
        {
            if (Pets.Any(savedPet => pet.Name.Equals(savedPet.Name)))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Pets.Add(pet);
            return pet;
        }
    }
}