using System.Collections.Generic;
using Library.Clinic.Models;

namespace Library.Clinic.Services
{
    public class TreatmentServiceProxy
    {
        private readonly List<Treatment> treatments;

        public TreatmentServiceProxy()
        {
            // Prepopulate with sample treatments
            treatments = new List<Treatment>
            {
                new Treatment { Name = "Physical Therapy", Price = 100m, Description = "Rehabilitation exercises", InsuranceDiscountPercentage = 10, InsuranceCoverageAmount = 20 },
                new Treatment { Name = "Chiropractic Adjustment", Price = 150m, Description = "Spinal alignment", InsuranceDiscountPercentage = 15, InsuranceCoverageAmount = 30 },
                new Treatment { Name = "Dental Cleaning", Price = 75m, Description = "Routine dental cleaning", InsuranceDiscountPercentage = 5, InsuranceCoverageAmount = 10 },
                new Treatment { Name = "Eye Exam", Price = 50m, Description = "Routine eye check-up", InsuranceDiscountPercentage = 8, InsuranceCoverageAmount = 15 }
            };
        }

        public IEnumerable<Treatment> GetTreatments() => treatments;

        public void AddTreatment(Treatment treatment) => treatments.Add(treatment);

        public void RemoveTreatment(string treatmentName) =>
            treatments.RemoveAll(t => t.Name == treatmentName);
    }
}
