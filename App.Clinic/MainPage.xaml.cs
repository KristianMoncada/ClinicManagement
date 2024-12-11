using System;
using Microsoft.Maui.Controls;

namespace App.Clinic
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Patients Navigation
        private async void Patients_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Patients");
        }

        // Physicians Navigation
        private async void Physicians_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//PhysicianManagement");
        }

        // Appointments Navigation
        private async void Appointments_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AppointmentManagement");
        }

        // Treatments Navigation
        private async void Treatments_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TreatmentManagement");
        }
    }
}
