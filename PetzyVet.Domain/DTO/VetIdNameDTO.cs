using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Domain.DTO
{
    public class VetIdNameDTO
    {
        public int VetId { get; set; }
        public string NPINumber { get; set; }

        public string Name { get; set; }
        public string Specialization { get; set; }
        public byte[] Photo { get; set; }
    }
}
