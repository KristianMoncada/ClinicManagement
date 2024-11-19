using System;

namespace Library.Clinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }

        public Appointment()
        {
            AppointmentDate = DateTime.Today;
        }

        public override string ToString()
        {
            return $"Appointment ID: {Id}, Patient ID: {PatientId}, Physician ID: {PhysicianId}, Date: {AppointmentDate}";
        }

        public static bool IsValidAppointmentTime(DateTime appointmentDate)
        {
            var time = appointmentDate.TimeOfDay;
            bool isWithinTimeRange = time >= TimeSpan.FromHours(8) && time < TimeSpan.FromHours(17); // Exclude exactly 5 PM
            bool isWeekday = appointmentDate.DayOfWeek >= DayOfWeek.Monday && appointmentDate.DayOfWeek <= DayOfWeek.Friday;

            return isWithinTimeRange && isWeekday;
        }
    }
}
