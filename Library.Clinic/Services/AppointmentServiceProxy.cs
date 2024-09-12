using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Clinic.Services
{
    public static class AppointmentServiceProxy
    {
        public static List<Appointment> Appointments { get; private set; } = new List<Appointment>();

        public static bool IsPhysicianAvailable(int physicianId, DateTime appointmentDate)
        {
            return !Appointments.Any(a => a.PhysicianId == physicianId && a.AppointmentDate == appointmentDate);
        }

        public static void AddAppointment(Appointment appointment)
        {
            if (!Appointment.IsValidAppointmentTime(appointment.AppointmentDate))
            {
                Console.WriteLine("Appointment must be between 8am and 5pm, Monday to Friday.");
                return;
            }

            if (!IsPhysicianAvailable(appointment.PhysicianId, appointment.AppointmentDate))
            {
                Console.WriteLine("Physician is already booked for this time.");
                return;
            }

            appointment.Id = Appointments.Count > 0 ? Appointments.Max(a => a.Id) + 1 : 1;
            Appointments.Add(appointment);
            Console.WriteLine("Appointment successfully added.");
        }

        public static void ListAllAppointments()
        {
            if (!Appointments.Any())
            {
                Console.WriteLine("No appointments available.");
                return;
            }

            foreach (var appointment in Appointments)
            {
                Console.WriteLine(appointment);
            }
        }

        public static List<Appointment> GetAppointmentsByPhysicianId(int physicianId)
        {
            return Appointments.Where(a => a.PhysicianId == physicianId).ToList();
        }
    }
}
