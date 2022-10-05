using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfSandbox.Commands;
using WpfSandbox.DataAccess;
using WpfSandbox.Interfaces.ViewModels;
using WpfSandbox.Models;
using WpfSandbox.Properties;

namespace WpfSandbox.ViewModels
{
    public class CustomerViewModel : WorkspaceViewModel, ICustomerViewModel
    {
        public string FirstName
        {
            get
            {
                return customer.FirstName;
            }
            set
            {
                if (value == customer.FirstName)
                {
                    return;
                }
                customer.FirstName = value;
                base.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return customer.LastName;
            }
            set
            {
                if (value == customer.LastName)
                {
                    return;
                }
                customer.LastName = value;
                base.OnPropertyChanged("LastName");
            }
        }

        public string Email
        {
            get
            {
                return customer.Email;
            }
            set
            {
                if (value == customer.Email)
                {
                    return;
                }
                customer.Email = value;
                base.OnPropertyChanged("Email");
            }
        }

        public double TotalSales
        {
            get
            {
                return customer.TotalSales;
            }
            set
            {
                if (value == customer.TotalSales)
                {
                    return;
                }
                customer.TotalSales = value;
                base.OnPropertyChanged("TotalSales");
            }
        }

        public bool IsCompany
        {
            get
            {
                return customer.IsCompany;
            }
            set
            {
                if (value == customer.IsCompany)
                {
                    return;
                }
                customer.IsCompany = value;
                base.OnPropertyChanged("IsCompany");
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (value == isSelected)
                {
                    return;
                }
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public string CustomerType
        {
            get
            {
                return customerType;
            }
            set
            {
                if (value == customerType || string.IsNullOrEmpty(value))
                {
                    return;
                }
                customerType = value;
                if (customerType == Resources.CustomerViewModel_CustomerTypeOption_Company)
                {
                    customer.IsCompany = true;
                }
                else if (customerType == Resources.CustomerViewModel_CustomerTypeOption_Person)
                {
                    customer.IsCompany = false;
                }
                OnPropertyChanged("CustomerType");
                OnPropertyChanged("LastName");
            }
        }

        public string[] CustomerTypeOptions
        {
            get
            {
                if (customerTypeOptions == null)
                {
                    customerTypeOptions = new string[]
                    {
                        Resources.CustomerViewModel_CustomerTypeOption_NotSpecified,
                        Resources.CustomerViewModel_CustomerTypeOption_Person,
                        Resources.CustomerViewModel_CustomerTypeOption_Company
                    };
                }
                return customerTypeOptions;
            }
        }

        public string TotalSalesText
        {
            get
            {
                return totalSalesText;
            }
            set
            {
                totalSalesText = value;
                if (double.TryParse(value, out double result))
                {
                    customer.TotalSales = result;
                }
                OnPropertyChanged("TotalSalesText");
                OnPropertyChanged("TotalSales");
            }
        }

        public override string DisplayName
        {
            get
            {
                if (IsNewCustomer)
                {
                    return Resources.CustomerViewModel_DisplayName;
                }
                else if (customer.IsCompany)
                {
                    return customer.FirstName;
                }
                else
                {
                    return $"{customer.LastName}, {customer.FirstName}";
                }
            }
        }

        string IDataErrorInfo.Error => (customer as IDataErrorInfo).Error;

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        private string GetValidationError(string propertyName)
        {
            string error;
            switch (propertyName)
            {
                case "CustomerType":
                    error = ValidateCustomerType();
                    break;
                case "TotalSalesText":
                    error = ValidateTotalSalesText();
                    break;
                default:
                    error = (customer as IDataErrorInfo)[propertyName];
                    break;
            }
            // Dirty the commands registered with the CommandManager, such as our Save command,
            // so that they are queried to see if they can execute now.
            CommandManager.InvalidateRequerySuggested();
            return error;
        }

        private string ValidateTotalSalesText()
        {
            if (string.IsNullOrEmpty(TotalSalesText))
            {
                return Resources.CustomerViewModel_Error_TotalSalesMissing;
            }
            if (!double.TryParse(TotalSalesText, out double _))
            {
                return Resources.CustomerViewModel_Error_TotalSalesInvalid;
            }
            return null;
        }

        public ICommand SaveCommand { get; private set; }

        #region constructors

        public CustomerViewModel(Customer customer, CustomerRepository customerRepository)
        {
            this.customer = customer ?? throw new ArgumentNullException("customer");
            this.customerRepository = customerRepository ?? throw new ArgumentNullException("customerRepository");
            customerType = Resources.CustomerViewModel_CustomerTypeOption_NotSpecified;
            totalSalesText = customer.TotalSales.ToString();
            SaveCommand = new RelayCommand(param => Save(), param => CanSave);
        }

        #endregion

        #region public methods

        public void Save()
        {
            if (!customer.IsValid)
            {
                throw new InvalidOperationException(Resources.CustomerViewModel_Exception_CannotSave);
            }
            if (IsNewCustomer)
            {
                customerRepository.AddCustomer(customer);
            }
            OnPropertyChanged("DisplayName");
        }

        public bool CanSave
        {
            get
            {
                return string.IsNullOrEmpty(ValidateCustomerType()) && customer.IsValid;
            }
        }

        #endregion

        #region private fields

        private string ValidateCustomerType()
        {
            return CustomerType == Resources.CustomerViewModel_CustomerTypeOption_Company
                || CustomerType == Resources.CustomerViewModel_CustomerTypeOption_Person
                ? null
                : Resources.CustomerViewModel_Error_MissingCustomerType;
        }

        #endregion

        #region private properties

        private bool IsNewCustomer
        {
            get
            {
                return !customerRepository.ContainsCustomer(customer);
            }
        }

        #endregion

        #region private fields

        private readonly Customer customer;
        private readonly CustomerRepository customerRepository;
        private string customerType;
        private string totalSalesText;
        private string[] customerTypeOptions;
        private bool isSelected;

        private static readonly string[] ValidatedProperties =
        {
            "CustomerType",
            "TotalSalesText"
        };

        #endregion
    }
}
