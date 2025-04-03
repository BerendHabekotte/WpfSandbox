using System.ComponentModel;

namespace DataGridTestApplication.Models
{
    public class UserOrganizationPersonalContactModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string telephone;
        public string Telephone
        {
            get => telephone;
            set
            {
                telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private int index;
        public int Index
        {
            get => index;
            set
            {
                index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
