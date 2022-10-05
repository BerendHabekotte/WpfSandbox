using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using WpfSandbox.Models;

namespace WpfSandbox.DataAccess
{
    public class CustomerRepository
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
            using (var stream = GetResourceStream(customerDataFile))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    return (
                        from customerElement in XDocument.Load(reader).Element("customers").Elements("customer")
                        select Customer.CreateCustomer(
                            (double)customerElement.Attribute("totalSales"),
                            (string)customerElement.Attribute("firstName"),
                            (string)customerElement.Attribute("lastName"),
                            (bool)customerElement.Attribute("isCompany"),
                            (string)customerElement.Attribute("email")
                            )).ToList();

                }
            }
        }

        private Stream GetResourceStream(string resourceFile)
        {
            var uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);
            var info = Application.GetResourceStream(uri);
            if (info == null || info.Stream == null)
            {
                throw new ApplicationException($"Missing resource file: {resourceFile}");
            }
            return info.Stream;
        }

        private readonly List<Customer> customers;
    }
}
