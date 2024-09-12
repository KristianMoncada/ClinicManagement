using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Clinic.Services
{
    public static class PatientServiceProxy
    {
        public static int lastKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public static List<Patient> Patients { get; private set; } = new List<Patient>();

        public static void AddPatient(Patient patient)
        {
            if (patient.Id <= 0)
            {
                patient.Id = lastKey + 1;
            }
            Patients.Add(patient);
        }

        public static void DeletePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);
            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
            }
        }
    }
}
