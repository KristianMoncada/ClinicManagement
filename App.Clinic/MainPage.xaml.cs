namespace App.Clinic
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void Patients_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Patients");
        }
    }

}
