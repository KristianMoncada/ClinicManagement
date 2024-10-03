using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Clinic.Services
{
    public class PatientServiceProxy
    {
        private static PatientServiceProxy? instance;

        private List<Patient> patients;

        private PatientServiceProxy()
        {
            patients = new List<Patient>
            {
                new Patient { Id = 1, Name = "John Doe" },
                new Patient { Id = 2, Name = "Jane Doe" }
            };
        }

        // This is the singleton instance accessor
        public static PatientServiceProxy Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientServiceProxy();
                }
                return instance;
            }
        }

        public List<Patient> Patients
        {
            get
            {
                return patients;
            }
        }

        public int LastKey
        {
            get
            {
                if (Patients.Any())
                {
                    return Patients.Max(x => x.Id);
                }
                return 0;
            }
        }

        public void AddOrUpdatePatient(Patient patient)
        {
            bool isAdd = false;
            if (patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                Patients.Add(patient);
            }
            
        }

        public void DeletePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id);
            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
            }
        }
    }
}
