using CustomerData.DataAccess;
using DiPresentationLogic;
using System;
using System.Collections.Generic;

namespace DiSandbox
{
    public class CustomerManagementAgent : ICustomerManagementAgent
    {
        private readonly ICustomerRepository repository;

        public CustomerManagementAgent(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public void DeleteCustomer(string firstName, string lastName)
        {
            repository.DeleteCustomer(firstName, lastName);
        }

        public void InsertCustomer(CustomerEditorViewModel customer)
        {
            var newCustomer = new CustomerData.Models.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                TotalSales = customer.TotalSales,
                IsCompany = customer.IsCompany
            };
            repository.AddCustomer(newCustomer);
        }

        public IEnumerable<CustomerViewModel> SelectAllCustomers()
        {
            var result = new List<CustomerViewModel>();
            var customers = repository.GetCustomers();
            foreach(var customer in customers)
            {
                result.Add(new CustomerViewModel
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    TotalSales = customer.TotalSales,
                    IsCompany = customer.IsCompany
                });
            }
            return result;
        }

        public void UpdateCustomer(CustomerEditorViewModel customer)
        {
            throw new NotImplementedException();
        }
    }
}
