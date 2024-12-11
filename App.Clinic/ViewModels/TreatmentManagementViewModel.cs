using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace App.Clinic.ViewModels
{
    public class TreatmentManagementViewModel : INotifyPropertyChanged
    {
        private readonly TreatmentServiceProxy _treatmentService;
        private string _newTreatmentName;
        private string _newTreatmentPrice;
        private string _insuranceDiscount;
        private string _insuranceCoverage;

        public ObservableCollection<Treatment> Treatments { get; set; }

        public string NewTreatmentName
        {
            get => _newTreatmentName;
            set
            {
                _newTreatmentName = value;
                OnPropertyChanged();
            }
        }

        public string NewTreatmentPrice
        {
            get => _newTreatmentPrice;
            set
            {
                _newTreatmentPrice = value;
                OnPropertyChanged();
            }
        }

        public string InsuranceDiscount
        {
            get => _insuranceDiscount;
            set
            {
                _insuranceDiscount = value;
                OnPropertyChanged();
            }
        }

        public string InsuranceCoverage
        {
            get => _insuranceCoverage;
            set
            {
                _insuranceCoverage = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTreatmentCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TreatmentManagementViewModel()
        {
            _treatmentService = new TreatmentServiceProxy();
            Treatments = new ObservableCollection<Treatment>(_treatmentService.GetTreatments());

            // Link the AddTreatmentCommand to the AddTreatment method
            AddTreatmentCommand = new Command(AddTreatment);
        }

        public void AddTreatment()
        {
            Console.WriteLine("Attempting to add treatment...");
            if (decimal.TryParse(NewTreatmentPrice, out decimal price) &&
                decimal.TryParse(InsuranceDiscount, out decimal discount) &&
                decimal.TryParse(InsuranceCoverage, out decimal coverage) &&
                !string.IsNullOrWhiteSpace(NewTreatmentName))
            {
                var treatment = new Treatment
                {
                    Name = NewTreatmentName,
                    Price = price,
                    Description = "Default description",
                    InsuranceDiscountPercentage = discount,
                    InsuranceCoverageAmount = coverage
                };

                Treatments.Add(treatment); // Add to ObservableCollection
                Console.WriteLine($"Treatment added: {treatment.Name}");

                // Reset input fields
                NewTreatmentName = string.Empty;
                NewTreatmentPrice = string.Empty;
                InsuranceDiscount = string.Empty;
                InsuranceCoverage = string.Empty;
            }
            else
            {
                Console.WriteLine("Failed to add treatment. Invalid input.");
            }
        }

        public void RemoveTreatment(Treatment treatment)
        {
            _treatmentService.RemoveTreatment(treatment.Name);
            Treatments.Remove(treatment);
        }

        public decimal CalculatePriceWithInsurance(Treatment treatment)
        {
            var discount = treatment.Price * (treatment.InsuranceDiscountPercentage / 100);
            var adjustedPrice = treatment.Price - discount - treatment.InsuranceCoverageAmount;
            return Math.Max(0, adjustedPrice); // Ensure no negative prices
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
