using Library.Clinic.Models;
using Library.Clinic.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace App.Clinic.ViewModels
{
    public class PatientManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PatientViewModel? SelectedPatient { get; set; }
        public string? Query { get; set; }

        public ObservableCollection<PatientViewModel> Patients
        {
            get
            {
                var retVal = new ObservableCollection<PatientViewModel>(PatientServiceProxy.Current.Patients
                    .Take(100)
                    .Where(p => p != null)
                    .Where(p => p.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty))
                    .Select(p => new PatientViewModel(p)));
                return retVal;
            }
        }

        public void Delete()
        {
            if (SelectedPatient == null)
            {
                return;
            }
            PatientServiceProxy.Current.DeletePatient(SelectedPatient.Id);

            Refresh();
        }

        public void UpdateInsuranceDetails(string planName, decimal discountPercentage, decimal coverageAmount)
        {
            if (SelectedPatient?.Model == null)
            {
                return;
            }

            SelectedPatient.Model.InsurancePlanName = planName;
            SelectedPatient.Model.InsuranceDiscountPercentage = discountPercentage;
            SelectedPatient.Model.InsuranceCoverageAmount = coverageAmount;

            // Save changes to the service
            PatientServiceProxy.Current.AddOrUpdatePatient(SelectedPatient.Model);
            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));
        }
    }
}
