using App.Clinic.ViewModels;
using Library.Clinic.Services;
using System;

namespace App.Clinic.Views
{
    public partial class AppointmentManagement : ContentPage
    {
        public AppointmentManagement()
        {
            InitializeComponent();
            BindingContext = new AppointmentManagementViewModel();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AppointmentDetails?appointmentId=0");
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var selectedAppointmentId = (BindingContext as AppointmentManagementViewModel)?.SelectedAppointment?.Id;
            if (selectedAppointmentId.HasValue)
            {
                await Shell.Current.GoToAsync($"//AppointmentDetails?appointmentId={selectedAppointmentId.Value}");
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var viewModel = (BindingContext as AppointmentManagementViewModel);
            if (viewModel != null && viewModel.SelectedAppointment != null)
            {
                AppointmentServiceProxy.DeleteAppointment(viewModel.SelectedAppointment.Id);
                viewModel.Refresh();
            }
        }

        private void AppointmentManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as AppointmentManagementViewModel)?.Refresh();
        }
    }
}
