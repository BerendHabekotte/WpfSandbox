using System.Collections.Generic;

namespace DiPresentationLogic
{
    public interface ICustomerManagementAgent
    {
        void DeleteCustomer(string firstName, string lastName);

        void InsertCustomer(CustomerEditorViewModel customer);

        IEnumerable<CustomerViewModel> SelectAllCustomers();

        void UpdateCustomer(CustomerEditorViewModel customer);
    }
}
