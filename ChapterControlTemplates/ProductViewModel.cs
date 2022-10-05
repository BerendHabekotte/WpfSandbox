using System;
using System.ComponentModel;

namespace ChapterControlTemplates
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string productCode = string.Empty;
        public string ProductCode
        {
            get => productCode;
            set
            {
                productCode = value;
                if (productCode.Length < 3)
                {
                    throw new ArgumentException("Productcode must be at least 3 characters long");
                }
                if (productCode.Length > 10)
                {
                    throw new ArgumentException("Productcode can not exceed 10 characters");
                }
                OnPropertyChanged("ProductCode");
            }
        }

        private string productName = "Fantastisch product!";
        public string ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged("ProductCode");
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
