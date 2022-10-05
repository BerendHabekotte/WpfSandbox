using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveChanges
{
    internal class MainWindowViewModel
    {
        private readonly CustomerViewModel customerViewModel;
        public CustomerViewModel CustomerViewModel { get { return customerViewModel; } }

        public MainWindowViewModel(CustomerViewModel customerViewModel)
        { 
            this.customerViewModel = customerViewModel;
        }
    }
}
