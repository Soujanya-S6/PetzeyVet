using PetzyVet.Data;
using PetzyVet.Data.Repositories;
using PetzyVet.Domain.DTO;
using PetzyVet.Domain.Entities;
using PetzyVet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Elmah;
using System.Web;
using System.Web.Http.Cors;

namespace PetzyVet.API.Controllers
{
    [RoutePrefix("api/vets")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VetsController : ApiController
    {
        public IVetRepository vetRepository = new VetRepository(new VetDbContext());
        public VetsController()
        {
            
        }

        private void LogError(string methodName, int? id = null, Exception ex = null)
        {
            Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
            Console.WriteLine($"Error in {methodName} API{(id.HasValue ? $" for ID: {id}" : "")}: {ex?.Message}");
        }

        public List<VetCardDTO> ConvertVetToVetCardDTO(List<Vet> vets)
        {
            List<VetCardDTO> vetCards = new List<VetCardDTO>();
            VetCardDTO vetCardDTO;
            foreach (Vet v in vets)
            {
                vetCardDTO = new VetCardDTO
                {
                    Name = v.FName + " " + v.LName,
                    PhoneNumber = v.Phone,
                    Speciality = v.Speciality,
                    Photo = v.Photo
                };
                vetCards.Add(vetCardDTO);
            }
            return vetCards;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllVets()
        {
            try
            {
                var vets = vetRepository.GetAllVets();
                if (vets == null || !vets.Any())
                {
                    return NotFound();
                }
                var vetCards = ConvertVetToVetCardDTO(vets);
                return Ok(vetCards);
            }
            catch (Exception ex)
            {
                LogError(nameof(GetAllVets), ex: ex);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetVetById(int id)
        {
            try
            {
                var vet = vetRepository.GetVetById(id);
                if (vet == null)
                {
                    return NotFound();
                }

                return Ok(new VetProfileDTO
                {
                    Name = vet.FName + " " + vet.LName,
                    NpiNumber = vet.NPINumber,
                    Speciality = vet.Speciality,
                    Email = vet.Email,
                    PhoneNumber = vet.Phone,
                    Photo= vet.Photo
                });
            }
            catch (Exception ex)
            {
                LogError(nameof(GetVetById), id, ex);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddVet([FromBody] Vet vet)
        {
            try
            {
                if (vet != null)
                {
                    vetRepository.AddVet(vet);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                LogError(nameof(AddVet), ex: ex);
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route(("{id}"))]
        public IHttpActionResult UpdateVet(int id, [FromBody] Vet vet)
        {
            try
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
            catch (Exception ex)
            {
                LogError(nameof(UpdateVet), id, ex);
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteVet(int id)
        {
            try
            {
                var vet = vetRepository.GetVetById(id);
                if (vet == null)
                {
                    return NotFound();
                }

                vetRepository.DeleteVet(id);
                return Ok();
            }
            catch (Exception ex)
            {
                LogError(nameof(DeleteVet), id, ex);
                return InternalServerError();
            }
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
                LogError(nameof(EditStatus), id, ex);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("VetDetails")]
        public IHttpActionResult GetVetsByListOfIds([FromBody] List<int> doctorIds)
        {
            try
            {
                if (doctorIds == null || !doctorIds.Any())
                {
                    return BadRequest("No doctor IDs provided.");
                }

                var doctorsDetails = new List<VetIdNameDTO>();

                foreach (var doctorId in doctorIds)
                {
                    var doctor = vetRepository.GetVetById(doctorId);

                    if (doctor != null)
                    {
                        doctorsDetails.Add(new VetIdNameDTO
                        {
                            VetId = doctor.VetId,
                            Name = doctor.FName + " " + doctor.LName,
                            Specialization = doctor.Speciality,
                            Photo = doctor.Photo
                        });
                    }
                }

                return Ok(doctorsDetails);
            }
            catch (Exception ex)
            {
                LogError(nameof(GetVetsByListOfIds), ex: ex);
                return InternalServerError();
            }
        }
    }
}
