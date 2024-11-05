using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Linq;

namespace App.Clinic.Views
{
    [QueryProperty(nameof(PhysicianId), "physicianId")]
    public partial class PhysicianView : ContentPage
    {
        public int PhysicianId { get; set; }

        public PhysicianView()
        {
            InitializeComponent();
        }

        private async void OnCancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//PhysicianManagement");
        }

        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            var physicianToSave = BindingContext as Physician;
            if (physicianToSave != null)
            {
                if (physicianToSave.Id == 0)
                {
                    PhysicianServiceProxy.AddPhysician(physicianToSave); // Add new physician
                }
                else
                {
                    PhysicianServiceProxy.UpdatePhysician(physicianToSave); // Update existing physician
                }
            }

            await Shell.Current.GoToAsync("//PhysicianManagement");
        }

        private void PhysicianView_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            if (PhysicianId > 0)
            {
                // Edit existing physician
                BindingContext = PhysicianServiceProxy.GetPhysicianById(PhysicianId);
            }
            else
            {
                // Add new physician
                BindingContext = new Physician();
            }
        }
    }
}

