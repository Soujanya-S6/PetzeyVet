using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Domain.Entities
{
    public class Vet
    {
        public int VetId { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string NPINumber { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Speciality { get; set; }
        public string ShortBio { get; set; }
        public bool Status { get; set; }
        public byte[] Photo { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public double Rating { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public int Counter {  get; set; } 

    }
}
