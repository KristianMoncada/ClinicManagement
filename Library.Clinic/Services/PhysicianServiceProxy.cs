using Library.Clinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Clinic.Services
{
    public static class PhysicianServiceProxy
    {
        public static List<Physician> Physicians { get; private set; } = new List<Physician>();

        public static void AddPhysician(Physician physician)
        {
            if (physician.Id == 0)
            {
                physician.Id = Physicians.Count > 0 ? Physicians.Max(p => p.Id) + 1 : 1;
                Physicians.Add(physician);
            }
        }

        public static void UpdatePhysician(Physician physician)
        {
            var existingPhysician = Physicians.FirstOrDefault(p => p.Id == physician.Id);
            if (existingPhysician != null)
            {
                existingPhysician.Name = physician.Name;
                existingPhysician.LicenseNumber = physician.LicenseNumber;
                existingPhysician.GraduationDate = physician.GraduationDate;
            }
        }

        public static void DeletePhysician(int id)
        {
            var physician = Physicians.FirstOrDefault(p => p.Id == id);
            if (physician != null)
            {
                Physicians.Remove(physician);
            }
        }

        public static Physician GetPhysicianById(int id)
        {
            return Physicians.FirstOrDefault(p => p.Id == id);
        }
    }
}
