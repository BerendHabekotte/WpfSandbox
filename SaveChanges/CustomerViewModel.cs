using BcWpfCommon.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaveChanges
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private CustomerModel model;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? CustomerId => model.CustomerId.ToString(); 

        public string? Name
        {
            get => model?.CustomerName;
            set
            {
                if (value == model.CustomerName)
                    return;
                model.CustomerName = value;
                OnPropertyChanged("Name");
            }
        }

        public string? Description
        {
            get => model?.Description;
            set 
            { 
                model.Description = value; 
                OnPropertyChanged("Description");
            }
        }

        public string ClearanceType
        { 
            get => model?.ClearanceType;
            set
            {
                model.ClearanceType = value;
                OnPropertyChanged("ClearanceType");
            }
        }

        public CustomerViewModel()
        {
            model = new CustomerModel 
            { 
                CustomerId = 1, 
                CustomerName = "Jansen", 
                Description = "Onze eerste klant",
                ClearanceType = null
            };
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ICommand LostFocusCommand
            => new ActionCommand<object>(
                    OnLostFocusExecute,
                    param => true);

        private void OnLostFocusExecute(object? obj)
        {
            if (obj == null)
                return;
            var parameters = (string[])obj; 
            var name = parameters[0];
            switch(name)
            {
                case "Name":
                    Name = parameters[1];
                    break;
                case "Description":
                    Description = parameters[1];
                    break;
                case "ClearanceType":
                    ClearanceType = parameters[1];
                    break;
                default:
                    break;
            }
        }
    }
}
