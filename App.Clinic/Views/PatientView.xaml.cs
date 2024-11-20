namespace App.Clinic.Views;

using global::App.Clinic.ViewModels;
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
        (BindingContext as PatientViewModel)?.ExecuteAdd();
    }

    private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if(PatientId > 0)
        {
            var model = PatientServiceProxy.Current.Patients.FirstOrDefault(p => p.Id == PatientId);
            if (model != null)
            {
                BindingContext = new PatientViewModel(model);
            }

            else
            {
                BindingContext = new PatientViewModel();
            }
        }
        else
        {
            BindingContext = new PatientViewModel();
        }
    }
}