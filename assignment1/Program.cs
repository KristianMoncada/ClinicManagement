using Library.Clinic.Models;
using Library.Clinic.Services;
using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool is_continue = true;
            do
            {
                Console.WriteLine("Medical Practice Management");
                Console.WriteLine("A. Add a patient");
                Console.WriteLine("B. Add a physician");
                Console.WriteLine("C. Create an appointment");
                Console.WriteLine("L. List all patients");
                Console.WriteLine("M. List all physicians");
                Console.WriteLine("N. List all appointments");
                Console.WriteLine("Q. Quit");

                string input = Console.ReadLine() ?? string.Empty;

                if (char.TryParse(input, out char choice))
                {
                    switch (choice)
                    {
                        case 'a':
                        case 'A':
                            Console.WriteLine("Enter patient name:");
                            var patientName = Console.ReadLine();

                            Console.WriteLine("Enter patient address:");
                            var patientAddress = Console.ReadLine();

                            Console.WriteLine("Enter patient birthdate (yyyy-mm-dd):");
                            DateTime patientBirthdate;
                            if (!DateTime.TryParse(Console.ReadLine(), out patientBirthdate))
                            {
                                Console.WriteLine("Invalid date format. Setting birthdate to default (01/01/0001).");
                                patientBirthdate = DateTime.MinValue;
                            }

                            Console.WriteLine("Enter patient SSN:");
                            var patientSSN = Console.ReadLine();

                            Console.WriteLine("Enter patient race:");
                            var patientRace = Console.ReadLine();

                            Console.WriteLine("Enter patient gender:");
                            var patientGender = Console.ReadLine();

                            var newPatient = new Patient
                            {
                                Name = patientName ?? string.Empty,
                                Address = patientAddress ?? string.Empty,
                                Birthday = patientBirthdate,
                                SSN = patientSSN ?? string.Empty,
                                Race = patientRace ?? string.Empty,
                                Gender = patientGender ?? string.Empty
                            };

                            bool addMoreNotes = true;
                            do
                            {
                                Console.WriteLine("Enter a diagnosis or prescription (or press Enter to skip):");
                                var note = Console.ReadLine();
                                if (!string.IsNullOrEmpty(note))
                                {
                                    newPatient.AddMedicalNotes(note);
                                }

                                Console.WriteLine("Do you want to add another medical note? (Y/N)");
                                var response = Console.ReadLine();
                                addMoreNotes = response?.Trim().ToLower() == "y";
                            } while (addMoreNotes);

                            PatientServiceProxy.AddPatient(newPatient);

                            Console.WriteLine($"Patient added successfully with ID: {newPatient.Id}");
                            break;

                        case 'b':
                        case 'B':
                            Console.WriteLine("Enter physician name:");
                            var physicianName = Console.ReadLine();

                            Console.WriteLine("Enter physician license number:");
                            var licenseNumber = Console.ReadLine();

                            Console.WriteLine("Enter physician graduation date (yyyy-mm-dd):");
                            DateTime graduationDate;
                            if (!DateTime.TryParse(Console.ReadLine(), out graduationDate))
                            {
                                Console.WriteLine("Invalid date format. Using default date.");
                                graduationDate = DateTime.MinValue;
                            }

                            Console.WriteLine("Enter physician specializations (comma-separated):");
                            var specializations = Console.ReadLine()?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>();

                            var newPhysician = new Physician
                            {
                                Name = physicianName ?? string.Empty,
                                LicenseNumber = licenseNumber ?? string.Empty,
                                GraduationDate = graduationDate,
                                Specializations = specializations
                            };

                            PhysicianServiceProxy.AddPhysician(newPhysician);

                            Console.WriteLine($"Physician added successfully with ID: {newPhysician.Id}");
                            break;

                        case 'c':
                        case 'C':
                            Console.WriteLine("Enter patient ID:");
                            if (!int.TryParse(Console.ReadLine(), out int patientId) || PatientServiceProxy.Patients.All(p => p.Id != patientId))
                            {
                                Console.WriteLine("Invalid patient ID.");
                                break;
                            }

                            Console.WriteLine("Enter physician ID:");
                            if (!int.TryParse(Console.ReadLine(), out int physicianId) || PhysicianServiceProxy.Physicians.All(p => p.Id != physicianId))
                            {
                                Console.WriteLine("Invalid physician ID.");
                                break;
                            }

                            Console.WriteLine("Enter appointment date and time (yyyy-mm-dd HH:mm):");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime appointmentDate))
                            {
                                Console.WriteLine("Invalid date and time format.");
                                break;
                            }

                            var newAppointment = new Appointment
                            {
                                PatientId = patientId,
                                PhysicianId = physicianId,
                                AppointmentDate = appointmentDate
                            };

                            AppointmentServiceProxy.AddAppointment(newAppointment);
                            break;

                        case 'l':
                        case 'L':
                            Console.WriteLine("Patients:");
                            PatientServiceProxy.Patients.ForEach(p => Console.WriteLine($"{p.Id}: {p.Name}"));
                            break;

                        case 'm':
                        case 'M':
                            Console.WriteLine("Physicians:");
                            PhysicianServiceProxy.Physicians.ForEach(p => Console.WriteLine($"{p.Id}: {p.Name}, License: {p.LicenseNumber}"));
                            break;

                        case 'n':
                        case 'N':
                            Console.WriteLine("Appointments:");
                            AppointmentServiceProxy.Appointments.ForEach(a =>
                            {
                                var patient = PatientServiceProxy.Patients.FirstOrDefault(p => p.Id == a.PatientId);
                                var physician = PhysicianServiceProxy.Physicians.FirstOrDefault(p => p.Id == a.PhysicianId);
                                Console.WriteLine($"Appointment ID: {a.Id}, Patient: {patient?.Name}, Physician: {physician?.Name}, Date: {a.AppointmentDate}");
                            });
                            break;

                        case 'q':
                        case 'Q':
                            is_continue = false;
                            break;

                        default:
                            Console.WriteLine("That is an invalid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            } while (is_continue);
        }
    }
}
