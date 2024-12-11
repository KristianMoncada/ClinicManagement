using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Windows.Input;

namespace App.Clinic.ViewModels
{
    public class PatientViewModel
    {
        public Patient? Model { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }

        public int Id
        {
            get => Model?.Id ?? -1;
            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                }
            }
        }

        public string Name
        {
            get => Model?.Name ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.Name = value;
                }
            }
        }

        // New Insurance Properties
        public string InsurancePlanName
        {
            get => Model?.InsurancePlanName ?? string.Empty;
            set
            {
                if (Model != null)
                {
                    Model.InsurancePlanName = value;
                }
            }
        }

        public decimal InsuranceDiscountPercentage
        {
            get => Model?.InsuranceDiscountPercentage ?? 0;
            set
            {
                if (Model != null)
                {
                    Model.InsuranceDiscountPercentage = value;
                }
            }
        }

        public decimal InsuranceCoverageAmount
        {
            get => Model?.InsuranceCoverageAmount ?? 0;
            set
            {
                if (Model != null)
                {
                    Model.InsuranceCoverageAmount = value;
                }
            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as PatientViewModel));
        }

        private void DoDelete()
        {
            if (Id > 0)
            {
                PatientServiceProxy.Current.DeletePatient(Id);
                Shell.Current.GoToAsync("//Patients");
            }
        }

        private void DoEdit(PatientViewModel? pvm)
        {
            if (pvm == null)
            {
                return;
            }
            var selectedPatientId = pvm?.Id ?? 0;
            Shell.Current.GoToAsync($"//PatientDetails?patientId={selectedPatientId}");
        }

        public PatientViewModel()
        {
            Model = new Patient();
            SetupCommands();
        }

        public PatientViewModel(Patient? _model)
        {
            Model = _model;
            SetupCommands();
        }

        public void ExecuteAdd()
        {
            if (Model != null)
            {
                PatientServiceProxy
                    .Current
                    .AddOrUpdatePatient(Model);
            }

            Shell.Current.GoToAsync("//Patients");
        }
    }
}
