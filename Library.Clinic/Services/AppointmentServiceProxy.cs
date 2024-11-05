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
            // Check if the PhysicianId exists in the PhysicianServiceProxy
            if (PhysicianServiceProxy.GetPhysicianById(appointment.PhysicianId) == null)
            {
                Console.WriteLine("Error: Physician ID does not exist.");
                return;
            }

            // Check if appointment time is valid
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

        public static void UpdateAppointment(Appointment updatedAppointment)
        {
            var existingAppointment = Appointments.FirstOrDefault(a => a.Id == updatedAppointment.Id);
            if (existingAppointment != null)
            {
                if (!Appointment.IsValidAppointmentTime(updatedAppointment.AppointmentDate))
                {
                    Console.WriteLine("Appointment must be between 8am and 5pm, Monday to Friday.");
                    return;
                }

                if (!IsPhysicianAvailable(updatedAppointment.PhysicianId, updatedAppointment.AppointmentDate))
                {
                    Console.WriteLine("Physician is already booked for this time.");
                    return;
                }

                // Update the existing appointment details
                existingAppointment.PatientId = updatedAppointment.PatientId;
                existingAppointment.PhysicianId = updatedAppointment.PhysicianId;
                existingAppointment.AppointmentDate = updatedAppointment.AppointmentDate;

                Console.WriteLine("Appointment successfully updated.");
            }
            else
            {
                Console.WriteLine("Appointment not found.");
            }
        }

        public static void DeleteAppointment(int id)
        {
            var appointment = Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                Appointments.Remove(appointment);
                Console.WriteLine("Appointment deleted successfully.");
            }
        }

        public static List<Appointment> GetAppointmentsByPhysicianId(int physicianId)
        {
            return Appointments.Where(a => a.PhysicianId == physicianId).ToList();
        }
    }
}
