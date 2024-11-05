using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.Clinic.ViewModels
{
    public class PhysicianManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Physician? SelectedPhysician { get; set; }

        // Use ObservableCollection to notify UI when data changes
        public ObservableCollection<Physician> Physicians { get; set; }

        public PhysicianManagementViewModel()
        {
            Physicians = new ObservableCollection<Physician>(PhysicianServiceProxy.Physicians);
        }

        public void Delete()
        {
            if (SelectedPhysician != null)
            {
                PhysicianServiceProxy.DeletePhysician(SelectedPhysician.Id);
                Refresh();
            }
        }

        // Refreshes the list of physicians
        public void Refresh()
        {
            Physicians = new ObservableCollection<Physician>(PhysicianServiceProxy.Physicians);
            NotifyPropertyChanged(nameof(Physicians));
        }
    }
}
