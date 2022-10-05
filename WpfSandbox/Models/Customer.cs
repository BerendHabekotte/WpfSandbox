using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using WpfSandbox.Properties;

namespace WpfSandbox.Models
{
    public class Customer : IDataErrorInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsCompany { get; set; }

        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                    {
                        return false;
                    }
                return true;
            }
        }

        public double TotalSales { get; set; }

        public bool IsSelected { get; set; }

        public ICommand SaveCommand { get; set; }

        public bool CanSave { get; set; }

        public static Customer CreateNewCustomer()
        {
            return new Customer();
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return GetValidationError(columnName);
            }
        }

        public static Customer CreateCustomer(
                            double totalSales,
                            string firstName,
                            string lastName,
                            bool isCompany,
                            string email)
        {
            return new Customer
            {
                TotalSales = totalSales,
                FirstName = firstName,
                LastName = lastName,
                IsCompany = isCompany,
                Email = email
            };
        }

        #region validation

        private string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
            {
                return null;
            }
            string error = null;
            switch (propertyName)
            {
                case "Email":
                    error = ValidateEmail();
                    break;
                case "FirstName":
                    error = ValidateFirstName();
                    break;
                case "LastName":
                    error = ValidateLastName();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on Customer: " + propertyName);
                    break;
            }
            return error;
        }

        private string ValidateEmail()
        {
            if (IsStringMissing(Email))
            {
                return Resources.Customer_Error_Missing_Email;
            }
            else if (!IsValidEmailAddress(Email))
            {
                return Resources.Customer_Error_InvalidEmail;
            }
            return null;
        }

        string ValidateFirstName()
        {
            if (IsStringMissing(FirstName))
            {
                return Resources.Customer_Error_MissingFirstName;
            }
            return null;
        }

        string ValidateLastName()
        {
            if (IsCompany)
            {
                if (!IsStringMissing(LastName))
                {
                    return Resources.Customer_Error_CompanyHasNoLastName;
                }
            }
            else
            {
                if (IsStringMissing(LastName))
                {
                    return Resources.Customer_Error_MissingLastName;
                }
            }
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return
                string.IsNullOrEmpty(value) ||
                value.Trim() == string.Empty;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // This regex pattern came from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private static readonly string[] ValidatedProperties =
                {
            "Email",
            "FirstName",
            "LastName",
        };

        #endregion // Validation

    }
}
