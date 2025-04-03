using System.ComponentModel;

namespace DataGridTestApplication.Models
{
    public class GoodsAttribute : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
