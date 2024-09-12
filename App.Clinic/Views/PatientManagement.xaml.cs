using Library.Clinic.Services;
using Library.Clinic.Models;
using System;

namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage
{
	public List<Patient> Patients
	{
		get
		{
			return PatientServiceProxy.Current.Patients;
		}
	}


	public PatientManagement()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private void Cancel_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
    }
}
