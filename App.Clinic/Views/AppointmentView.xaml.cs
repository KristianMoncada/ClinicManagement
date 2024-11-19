using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Linq;

namespace App.Clinic.Views
{
    [QueryProperty(nameof(AppointmentId), "appointmentId")]
    public partial class AppointmentView : ContentPage
    {
        public int AppointmentId { get; set; }

        // Bindable property to handle appointment time with two-way binding
        public static readonly BindableProperty AppointmentTimeProperty =
            BindableProperty.Create(nameof(AppointmentTime), typeof(TimeSpan), typeof(AppointmentView), default(TimeSpan), BindingMode.TwoWay);

        public TimeSpan AppointmentTime
        {
            get => (TimeSpan)GetValue(AppointmentTimeProperty);
            set => SetValue(AppointmentTimeProperty, value);
        }

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
                // Combine the selected date and time
                appointmentToSave.AppointmentDate = appointmentToSave.AppointmentDate.Date + appointmentToSave.AppointmentTime;

                // Validate appointment date and time
                if (!Appointment.IsValidAppointmentTime(appointmentToSave.AppointmentDate))
                {
                    await DisplayAlert("Error", "Appointment must be between 8 AM and 5 PM, Monday to Friday.", "OK");
                    return;
                }

                // Add or update the appointment
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
                // Load existing appointment for editing
                var existingAppointment = AppointmentServiceProxy.Appointments.FirstOrDefault(a => a.Id == AppointmentId);
                BindingContext = existingAppointment;

                // Set AppointmentTime from existing appointment or keep default if null
                AppointmentTime = existingAppointment?.AppointmentDate.TimeOfDay ?? AppointmentTime;
            }
            else
            {
                // Initialize a new appointment with default date and time only if creating a new one
                var newAppointment = new Appointment { AppointmentDate = DateTime.Today };
                BindingContext = newAppointment;
            }
        }
    }
}
