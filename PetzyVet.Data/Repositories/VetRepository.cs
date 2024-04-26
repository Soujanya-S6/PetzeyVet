using PetzyVet.Domain.Entities;
using PetzyVet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Data.Repositories
{
    public class VetRepository : IVetRepository
    {
        private VetDbContext db;
        public VetRepository(VetDbContext db)
        {
            this.db = db;
        }
        public void AddVet(Vet vet)
        {
            db.Vets.Add(vet);
            db.SaveChanges();
        }

        public void DeleteVet(int id)
        {
            db.Vets.Remove(db.Vets.Find(id));
            db.SaveChanges();
        }

        public void EditStatus(bool status, int id)
        {
            Vet existingVet = db.Vets.Find(id);

            if (existingVet != null)
            {
                existingVet.Status = status;

                db.Entry(existingVet).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public List<Vet> GetAllVets()
        {
            return db.Vets.ToList();
        }

        public Vet GetVetById(int id)
        {
            return db.Vets.Find(id);
        }

        public void UpdateVet(Vet vet)
        {
            db.Entry(vet).State=EntityState.Modified;
            db.SaveChanges();
        }
    }
}
