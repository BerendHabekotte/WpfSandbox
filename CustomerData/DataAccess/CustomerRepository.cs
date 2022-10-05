using CustomerData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace CustomerData.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(string customerDataFile)
        {
            customers = LoadCustomers(customerDataFile);
        }

        public event EventHandler<CustomerAddedEventArgs> CustomerAdded;

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            if (!customers.Contains(customer))
            {
                customers.Add(customer);
                CustomerAdded?.Invoke(this, new CustomerAddedEventArgs(customer));
            }
        }

        public void DeleteCustomer(string firstName, string lastName)
        {
            var customer = customers.Where(c => c.FirstName == firstName && c.LastName == lastName).FirstOrDefault();
            if (customer == null)
            {
                throw new Exception($"Customer {firstName} {lastName} not found");
            }
            customers.Remove(customer);
        }

        public bool ContainsCustomer(Customer customer)
        {
            return customer == null ? throw new ArgumentNullException("customer") : customers.Contains(customer);
        }

        public List<Customer> GetCustomers()
        {
            return new List<Customer>(customers);
        }

        private List<Customer> LoadCustomers(string customerDataFile)
        {
            var document = new XmlDocument();
            document.Load(customerDataFile);
            var customers = document.SelectNodes("/customers/customer");
            var result = new List<Customer>();
            foreach (XmlNode customer in customers)
            {
                result.Add(Customer.CreateCustomer(
                    double.Parse(customer.Attributes["totalSales"].Value),
                    customer.Attributes["firstName"].InnerText,
                    customer.Attributes["lastName"].InnerText,
                    bool.Parse(customer.Attributes["isCompany"].Value),
                    customer.Attributes["email"].InnerText
                    ));
            }
            return result;
        }

        private readonly List<Customer> customers;
    }
}
