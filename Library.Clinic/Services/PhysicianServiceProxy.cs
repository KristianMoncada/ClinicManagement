using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Clinic.Services
{
    public static class PhysicianServiceProxy
    {
        public static int lastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public static List<Physician> Physicians { get; private set; } = new List<Physician>();

        public static void AddPhysician(Physician physician)
        {
            if (physician.Id <= 0)
            {
                physician.Id = lastKey + 1;
            }
            Physicians.Add(physician);
        }

        public static void DeletePhysician(int id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(p => p.Id == id);
            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);
            }
            else
            {
                Console.WriteLine($"Physician with ID {id} not found.");
            }
        }

        public static Physician GetPhysicianById(int id)
        {
            return Physicians.FirstOrDefault(p => p.Id == id);
        }

        public static void ListAllPhysicians()
        {
            if (!Physicians.Any())
            {
                Console.WriteLine("No physicians available.");
                return;
            }

            foreach (var physician in Physicians)
            {
                Console.WriteLine(physician);
            }
        }
    }
}
