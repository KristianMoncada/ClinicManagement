using App.Clinic.ViewModels;

namespace App.Clinic.Views
{
    public partial class TreatmentManagementView : ContentPage
    {
        public TreatmentManagementView()
        {
            InitializeComponent();
            BindingContext = new TreatmentManagementViewModel(); // Set BindingContext in code
        }

        // Back Button Handler
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage"); // Navigate back to Home
        }
    }
}
