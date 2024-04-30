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
        public string Name {  get; set; }
        public string NpiNumber { get; set; }
        public string Speciality { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo  { get; set; }
    }
}
