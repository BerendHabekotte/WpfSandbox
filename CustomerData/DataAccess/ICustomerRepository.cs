using CustomerData.Models;
using System.Collections.Generic;

namespace CustomerData.DataAccess
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);

        void DeleteCustomer(string firstName, string lastName);

        bool ContainsCustomer(Customer customer);

        List<Customer> GetCustomers();
    }
}
