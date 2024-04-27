using PetzyVet.Data;
using PetzyVet.Data.Repositories;
using PetzyVet.Domain.Entities;
using PetzyVet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace PetzyVet.API.Controllers
{
    [RoutePrefix("api/vets")]
    public class VetsController : ApiController
    {
        public IVetRepository vetRepository=new VetRepository();

        [HttpGet]
        public IHttpActionResult GetAllVets()
        {
            var vets = vetRepository.GetAllVets();
            if (vets == null) { return NotFound(); }
            return Ok(vets);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetVetById(int id)
        {
            var vet = vetRepository.GetVetById(id);
            if (vet == null)
            {
                return NotFound();
            }
            return Ok(vet);
        }

        [HttpPost]
        public IHttpActionResult AddVet([FromBody] Vet vet)
        {
            vetRepository.AddVet(vet);
            return Ok();
        }

        [HttpPut]
        [Route(("{id}"))]
        public IHttpActionResult UpdateVet(int id, [FromBody] Vet vet)
        {
            if (id != vet.VetId)
            {
                return BadRequest("Vet ID in the request body does not match the ID in the URL.");
            }

            var existingVet = vetRepository.GetVetById(id);
            if (existingVet == null)
            {
                return NotFound();
            }

            vetRepository.UpdateVet(vet);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteVet(int id)
        {
            var vet = vetRepository.GetVetById(id);
            if (vet == null)
            {
                return NotFound();
            }

            vetRepository.DeleteVet(id);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult EditStatus(int id, [FromBody] bool status)
        {
            try
            {
                vetRepository.EditStatus(status, id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return InternalServerError();
            }
        }


    }
}
