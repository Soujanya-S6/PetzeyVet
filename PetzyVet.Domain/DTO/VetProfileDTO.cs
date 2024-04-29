using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Domain.DTO
{
    public class VetProfileDTO
    {
        public string Name {  get; set; }
        public string NpiNumber { get; set; }
        public string Speciality { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl  { get; set; }
    }
}
