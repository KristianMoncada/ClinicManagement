using App.Clinic.ViewModels;
using System;

namespace App.Clinic.Views
{
    public partial class PhysicianManagement : ContentPage
    {
        public PhysicianManagement()
        {
            InitializeComponent();
            BindingContext = new PhysicianManagementViewModel();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//PhysicianDetails?physicianId=0");
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var selectedPhysicianId = (BindingContext as PhysicianManagementViewModel)?.SelectedPhysician?.Id;
            if (selectedPhysicianId.HasValue)
            {
                await Shell.Current.GoToAsync($"//PhysicianDetails?physicianId={selectedPhysicianId.Value}");
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            (BindingContext as PhysicianManagementViewModel)?.Delete();
        }

        private void PhysicianManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as PhysicianManagementViewModel)?.Refresh();
        }
    }
}
