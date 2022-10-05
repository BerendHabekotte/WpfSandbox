using System;
using WpfSandbox.Models;

namespace WpfSandbox.DataAccess
{
    /// <summary>
    /// Event arguments used by the CustomerRepository's CustomerAdded event.
    /// </summary>
    public class CustomerAddedEventArgs : EventArgs
    {
        public CustomerAddedEventArgs(Customer newCustomer)
        {
            NewCustomer = newCustomer;
        }

        public Customer NewCustomer { get; private set; }
    }
}
