namespace App.Clinic.Views;
using Library.Clinic.Models;
using Library.Clinic.Services;

[QueryProperty(nameof(PatientId), "patientId")]

public partial class PatientView : ContentPage
{
	public PatientView()
	{
		InitializeComponent();
    }

    public int PatientId { get; set; }

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
            .AddOrUpdatePatient(BindingContext as Patient);
        }

        Shell.Current.GoToAsync("//Patients");
    }

    private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if(PatientId > 0)
        {
            BindingContext = PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == PatientId);
        }
        else
        {
            BindingContext = new Patient();
        }
    }
}