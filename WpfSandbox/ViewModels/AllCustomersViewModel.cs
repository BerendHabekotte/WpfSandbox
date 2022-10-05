using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSandbox.DataAccess;
using WpfSandbox.Interfaces.ViewModels;
using WpfSandbox.Properties;

namespace WpfSandbox.ViewModels
{
    public class AllCustomersViewModel : WorkspaceViewModel, IAllCustomersViewModel
    {
        #region public properties

        public ObservableCollection<CustomerViewModel> AllCustomers { get; private set; }

        public double TotalSelectedSales
        {
            get
            {
                return AllCustomers.Sum(vm => vm.IsSelected ? vm.TotalSales : 0.0);
            }
        }

        #endregion

        #region constructors

        public AllCustomersViewModel(CustomerRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            DisplayName = Resources.AllCustomersViewModel_DisplayName;
            this.repository = repository;
            repository.CustomerAdded += OnCustomerAdded;
            CreateAllCustomers();
        }

        private void CreateAllCustomers()
        {
            List<CustomerViewModel> all =
                (from cust in repository.GetCustomers()
                 select new CustomerViewModel(cust, repository))
                 .ToList();
            foreach(var viewModel in all)
            {
                viewModel.PropertyChanged += OnCustomerViewModelPropertyChanged;
            }
            AllCustomers = new ObservableCollection<CustomerViewModel>(all);
            AllCustomers.CollectionChanged += OnCollectionChanged;
        }

        #endregion

        #region overrides

        protected override void OnDispose()
        {
            foreach (var viewModel in AllCustomers)
            {
                viewModel.Dispose();
            }
            AllCustomers.Clear();
            AllCustomers.CollectionChanged -= OnCollectionChanged;
            repository.CustomerAdded -= OnCustomerAdded;
        }

        #endregion

        private void OnCustomerViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string isSelected = "IsSelected";
            (sender as CustomerViewModel).VerifyPropertyName(isSelected);
            if (e.PropertyName == isSelected)
            {
                OnPropertyChanged("TotalSelectedSales");
            }
        }

        #region event handlers

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {
                foreach (CustomerViewModel viewModel in e.NewItems)
                {
                    viewModel.PropertyChanged += OnCustomerViewModelPropertyChanged;
                }
            }
            if (e.OldItems != null && e.OldItems.Count != 0)
            {
                foreach (CustomerViewModel viewModel in e.OldItems)
                {
                    viewModel.PropertyChanged -= OnCustomerViewModelPropertyChanged;
                }
            }
        }

        private void OnCustomerAdded(object sender, CustomerAddedEventArgs e)
        {
            var viewModel = new CustomerViewModel(e.NewCustomer, repository);
            AllCustomers.Add(viewModel);
        }

        #endregion

        private readonly CustomerRepository repository;
    }
}
