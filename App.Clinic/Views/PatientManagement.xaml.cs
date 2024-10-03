using Library.Clinic.Services;
using Library.Clinic.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Clinic.ViewModels;

namespace App.Clinic.Views;

public partial class PatientManagement : ContentPage, INotifyPropertyChanged
{
	public PatientManagement()
	{
		InitializeComponent();
		BindingContext = new PatientManagementViewModel();
	}

	private void Cancel_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
    }

    private void Add_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//PatientDetails");
    }

    private void Edit_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PatientDetails");
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
		(BindingContext as PatientManagementViewModel)?.Delete();
    }

    private void PatientManagement_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		(BindingContext as PatientManagementViewModel)? .Refresh();
    }

}
