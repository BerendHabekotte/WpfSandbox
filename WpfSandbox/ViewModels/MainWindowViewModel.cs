using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using WpfSandbox.Commands;
using WpfSandbox.DataAccess;
using WpfSandbox.Interfaces.ViewModels;
using WpfSandbox.Models;

namespace WpfSandbox.ViewModels
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        public ReadOnlyCollection<ICommandViewModel> Commands { get; private set; }

        public ObservableCollection<WorkspaceViewModel> Workspaces { get; private set; }

        public MainWindowViewModel(string path)
        {
            repository = new CustomerRepository(path);
            Workspaces = new ObservableCollection<WorkspaceViewModel>();
            Workspaces.CollectionChanged += OnWorkspaceChanged;
            CreateCommands();
        }

        private void OnWorkspaceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
            {
                foreach (WorkspaceViewModel workspace in e.NewItems)
                {
                    workspace.RequestClose += OnWorkspaceRequestClose;
                }
            }
            if (e.OldItems != null && e.OldItems.Count != 0)
            {
                foreach(WorkspaceViewModel workspace in e.OldItems)
                {
                    workspace.RequestClose -= OnWorkspaceRequestClose;
                }
            }
        }

        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            var workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }

        private void CreateCommands()
        {
            var commands = new List<ICommandViewModel>();
            var command = new CommandViewModel("View all customers", new RelayCommand(x => ShowAllCustomers()));
            commands.Add(command);
            command = new CommandViewModel("Add customers", new RelayCommand(x => AddCustomer()));
            commands.Add(command);
            Commands = new ReadOnlyCollection<ICommandViewModel>(commands);
        }

        private void ShowAllCustomers()
        {
            AllCustomersViewModel workspace = Workspaces.FirstOrDefault(vm => vm is AllCustomersViewModel) as AllCustomersViewModel;
            if (workspace == null)
            {
                workspace = new AllCustomersViewModel(repository);
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Workspaces.Contains(workspace));
            var collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(workspace);
            }
        }

        private void AddCustomer()
        {
            var newCustomer = Customer.CreateNewCustomer();
            var workspace = new CustomerViewModel(newCustomer, repository);
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        private readonly CustomerRepository repository;
    }
}
