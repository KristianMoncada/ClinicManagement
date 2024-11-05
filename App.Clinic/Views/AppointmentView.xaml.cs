using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Linq;

namespace App.Clinic.Views
{
    [QueryProperty(nameof(AppointmentId), "appointmentId")]
    public partial class AppointmentView : ContentPage
    {
        public int AppointmentId { get; set; }

        // Property to handle appointment time
        public TimeSpan AppointmentTime { get; set; }

        public AppointmentView()
        {
            InitializeComponent();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AppointmentManagement");
        }

        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            var appointmentToSave = BindingContext as Appointment;
            if (appointmentToSave != null)
            {
                // Combine the selected date and time for the appointment
                appointmentToSave.AppointmentDate = appointmentToSave.AppointmentDate.Date + AppointmentTime;

                if (!Appointment.IsValidAppointmentTime(appointmentToSave.AppointmentDate))
                {
                    await DisplayAlert("Error", "Appointment must be between 8 AM and 5 PM, Monday to Friday.", "OK");
                    return;
                }

                if (appointmentToSave.Id == 0)
                {
                    AppointmentServiceProxy.AddAppointment(appointmentToSave);
                }
                else
                {
                    AppointmentServiceProxy.UpdateAppointment(appointmentToSave);
                }
            }

            await Shell.Current.GoToAsync("//AppointmentManagement");
        }

        private void AppointmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (AppointmentId > 0)
            {
                var existingAppointment = AppointmentServiceProxy.Appointments.FirstOrDefault(a => a.Id == AppointmentId);
                BindingContext = existingAppointment;
                AppointmentTime = existingAppointment?.AppointmentDate.TimeOfDay ?? new TimeSpan(9, 0, 0);
            }
            else
            {
                BindingContext = new Appointment();
                AppointmentTime = new TimeSpan(9, 0, 0); // Default time set to 9 AM
            }
        }
    }
}
