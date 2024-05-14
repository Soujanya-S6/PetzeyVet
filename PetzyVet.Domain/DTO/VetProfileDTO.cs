using PetzyVet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Domain.DTO
{
    public class VetProfileDTO
    {
        public int VetId { get; set; }
        public string NPINumber { get; set; }
        public string FName {  get; set; }
        public string LName { get; set; }
        public string Speciality { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo  { get; set; }
        public bool Status { get; set; }
        public Address address { get; set; }
    }
}
