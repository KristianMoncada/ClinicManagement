using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.ViewModels
{
    public class AppointmentManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Appointment? SelectedAppointment { get; set; }

        // ObservableCollection to notify UI when data changes
        public ObservableCollection<Appointment> Appointments { get; set; }

        public AppointmentManagementViewModel()
        {
            Appointments = new ObservableCollection<Appointment>(AppointmentServiceProxy.Appointments);
        }

        // Property for binding the date part of the appointment
        public DateTime StartDate
        {
            get => SelectedAppointment?.AppointmentDate.Date ?? DateTime.Today;
            set
            {
                if (SelectedAppointment != null)
                {
                    SelectedAppointment.AppointmentDate = value + SelectedAppointment.AppointmentDate.TimeOfDay;
                    NotifyPropertyChanged();
                }
            }
        }

        // Property for binding the time part of the appointment
        public TimeSpan StartTime
        {
            get => SelectedAppointment?.AppointmentDate.TimeOfDay ?? default;
            set
            {
                if (SelectedAppointment != null)
                {
                    SelectedAppointment.AppointmentDate = SelectedAppointment.AppointmentDate.Date + value;
                    NotifyPropertyChanged();
                }
            }
        }

        // This method updates the AppointmentDate with the combined date and time
        public void RefreshTime()
        {
            if (SelectedAppointment != null)
            {
                SelectedAppointment.AppointmentDate = StartDate + StartTime;
            }
        }

        public void Delete()
        {
            if (SelectedAppointment != null)
            {
                AppointmentServiceProxy.DeleteAppointment(SelectedAppointment.Id);
                Refresh();
            }
        }

        public void Refresh()
        {
            Appointments = new ObservableCollection<Appointment>(AppointmentServiceProxy.Appointments);
            NotifyPropertyChanged(nameof(Appointments));
        }

        public void AddOrUpdate()
        {
            if (SelectedAppointment != null)
            {
                RefreshTime(); // Ensure the date and time are combined before saving
                if (SelectedAppointment.Id == 0)
                {
                    AppointmentServiceProxy.AddAppointment(SelectedAppointment);
                }
                else
                {
                    AppointmentServiceProxy.UpdateAppointment(SelectedAppointment);
                }
                Refresh();
            }
        }
    }
}
