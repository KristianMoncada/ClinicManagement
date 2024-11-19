using System;
using System.Collections.Generic;

namespace Library.Clinic.Models
{
    public class Patient
    {
        public override string ToString()
        {
            return Display;
        }

        public string Display
        {
            get
            {
                return $"[{Id}] {Name}";
            }
        }
        public int Id { get; set; }
        private string? name;
        public string Name
        {
            get
            {
                return name ?? string.Empty;
            }
            set
            {
                name = value;
            }
        }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        private List<string> MedicalNotes { get; set; }

        public Patient()
        {
            name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            SSN = string.Empty;
            Race = string.Empty;
            Gender = string.Empty;
            MedicalNotes = new List<string>();
        }

        public void AddMedicalNotes(string note)
        {
            MedicalNotes.Add(note);
        }
    }
}
