using System;
using CustomerData.Models;

namespace CustomerData.DataAccess
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
