using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DiPresentationLogic
{
    public class MainWindowViewModel
    {
        private readonly ICustomerManagementAgent agent;
        private readonly IWindow window;
        private readonly ObservableCollection<CustomerViewModel> customers;
        private readonly RelayCommand closeCommand;
        private readonly RelayCommand deleteCustomerCommand;
        private readonly RelayCommand editCustomerCommand;
        private readonly RelayCommand insertCustomerCommand;
        private readonly RelayCommand refreshCommand;

        public ObservableCollection<CustomerViewModel> Customers
        {
            get => customers;
        }

        public ICommand CloseCommand
        {
            get { return closeCommand; }
        }

        public ICommand DeleteCustomerCommand
        {
            get { return deleteCustomerCommand; }
        }

        public ICommand EditCustomerCommand
        {
            get { return editCustomerCommand; }
        }

        public ICommand InsertCustomerCommand
        {
            get { return insertCustomerCommand; }
        }

        public ICommand RefreshCommand
        {
            get { return refreshCommand; }
        }


        public MainWindowViewModel(ICustomerManagementAgent agent, IWindow window)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent");
            }
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }
            this.agent = agent;
            this.window = window;
            customers = new ObservableCollection<CustomerViewModel>();
            closeCommand = new RelayCommand(Close);
            deleteCustomerCommand = new RelayCommand(DeleteCustomer, IsCustomerSelected);
            editCustomerCommand = new RelayCommand(EditCustomer, IsCustomerSelected);
            insertCustomerCommand = new RelayCommand(InsertCustomer);
            refreshCommand = new RelayCommand(Refresh);
        }


        private void Close(object parameter)
        {
            this.window.Close();
        }

        private void DeleteCustomer(object parameter)
        {
            var customer = Customers.Single(p => p.IsSelected);
            agent.DeleteCustomer(customer.FirstName, customer.LastName);
            Refresh(new object());
        }

        private bool IsCustomerSelected(object parameter)
        {
            return Customers.Where(p => p.IsSelected).Count() == 1;
        }

        private void EditCustomer(object parameter)
        {
            var customer = Customers.Single(p => p.IsSelected);
            var editor = customer.Edit();
            editor.Title = "Edit Customer";

            if (window.CreateChild(editor).ShowDialog() ?? false)
            {
                agent.UpdateCustomer(editor);
                Refresh(new object());
            }
        }

        private void InsertCustomer(object parameter)
        {
            var editor = new CustomerEditorViewModel(string.Empty, string.Empty);
            editor.Title = "Add Customer";
            if (window.CreateChild(editor).ShowDialog() ?? false)
            {
                agent.InsertCustomer(editor);
                Refresh(new object());
            }
        }

        private void Refresh(object parameter)
        {
            customers.Clear();
            foreach (var customer in agent.SelectAllCustomers())
            {
                customers.Add(customer);
            }
        }
    }
}
