using System;
using System.Collections.Generic;

namespace Library.Clinic.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<string> Specializations { get; set; } = new List<string>();

        public Physician()
        {
            Name = string.Empty;
            LicenseNumber = string.Empty;
            GraduationDate = DateTime.MinValue;
        }

        public override string ToString()
        {
            var specializations = string.Join(", ", Specializations);
            return $"Id: {Id}, Name: {Name}, License Number: {LicenseNumber}, Graduation Date: {GraduationDate.ToShortDateString()}, Specializations: {specializations}";
        }
    }
}
