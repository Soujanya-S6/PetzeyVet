using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Domain.DTO
{
    public class VetCardDTO
    {
        public string Name {  get; set; }
        public string PhoneNumber { get; set; }
        public string Speciality { get; set; }
        public byte[] Photo { get; set; }
    }
}
