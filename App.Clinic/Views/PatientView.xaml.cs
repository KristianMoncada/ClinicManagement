namespace App.Clinic.Views;
using Library.Clinic.Models;
using Library.Clinic.Services;


public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Patients");
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        var patientToAdd = BindingContext as Patient;
        if (patientToAdd != null)
        {
            PatientServiceProxy
            .Current
            .AddPatient(BindingContext as Patient);
        }

        Shell.Current.GoToAsync("//Patients");
    }

    private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if( )
        BindingContext = PatientServiceProxy.Current.Patients.FirstOrDefault();
    }
}