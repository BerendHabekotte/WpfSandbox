using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DiPresentationLogic
{
    public class CustomerEditorViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string email;
        private bool isCompany;
        private double totalSales;
        private string title;

        public CustomerEditorViewModel(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            totalSales = 0;
            title = string.Empty;
        }

        public bool IsValid
        {
            get
            {
                return IsNameValid && IsEmailValid;
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (firstName != value)
                {
                    var wasValid = IsValid;
                    firstName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FirstName"));
                    NotifyValid(wasValid);
                }
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (lastName != value)
                {
                    var wasValid = IsValid;
                    lastName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
                    NotifyValid(wasValid);
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (email != value)
                {
                    var wasValid = IsValid;
                    email = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LastName"));
                    NotifyValid(wasValid);
                }
            }
        }

        public bool IsCompany
        {
            get => isCompany;
            set
            {
                if (isCompany != value)
                {
                    isCompany = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsCompany"));
                }
            }
        }

        public double TotalSales
        {
            get => totalSales;
            set
            {
                if (totalSales != value)
                {
                    totalSales = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("TotalSales"));
                }
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (title != value)
                {
                    var wasValid = IsValid;
                    title = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Title"));
                    NotifyValid(wasValid);
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "FirstName":
                    case "LastName":
                        return IsNameValid ? string.Empty : "First name and last name must not be empty.";
                    case "Email":
                        return IsEmailValid ? string.Empty : "Please enter a valid email address.";
                    default:
                        return string.Empty;
                }
            }
        }


        public string Error => IsValid ? string.Empty : "One or more values are invalid.";


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private bool IsNameValid => FirstName.Length > 0 && LastName.Length > 0;

        private bool IsEmailValid
        {
            get
            {
                if (email == null)
                {
                    return false;
                }
                if (Email.Length == 0)
                {
                    return false;
                }
                // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
                string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
        }

        private void NotifyValid(bool wasValid)
        {
            if (IsValid != wasValid)
            {
                OnPropertyChanged(new PropertyChangedEventArgs("IsValid"));
            }
        }
    }
}
