using Library.Clinic.Services;
using Library.Clinic.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

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

    private void Add_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//PatientDetails");
    }
}
