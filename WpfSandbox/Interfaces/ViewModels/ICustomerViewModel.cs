using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfSandbox.Interfaces.ViewModels
{
    public interface ICustomerViewModel : IDataErrorInfo
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        double TotalSales { get; set; }
        bool IsCompany { get; set; }
        bool IsSelected { get; set; }
        string CustomerType { get; set; }
        string[] CustomerTypeOptions { get; }
        string TotalSalesText { get; set; }
        string DisplayName { get; set; }
        ICommand SaveCommand { get; }
        bool CanSave { get; }

        void Save();

    }
}
