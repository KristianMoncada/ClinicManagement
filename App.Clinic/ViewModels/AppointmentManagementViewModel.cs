using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.ViewModels
{
    public class AppointmentManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Appointment? SelectedAppointment { get; set; }

        // Use ObservableCollection to notify UI when data changes
        public ObservableCollection<Appointment> Appointments { get; set; }

        public AppointmentManagementViewModel()
        {
            Appointments = new ObservableCollection<Appointment>(AppointmentServiceProxy.Appointments);
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
    }
}
