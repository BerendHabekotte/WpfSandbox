﻿using BcWpfCommon.Commands;
using DataGridTestApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using DataGridTestApplication.Views;

namespace DataGridTestApplication.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer
        {
            get => selectedIndex < 0 ? null : Customers[selectedIndex];
            set
            {
                selectedIndex = Customers.IndexOf(value);
                OnPropertyChanged(nameof(SelectedCustomer));
                OnPropertyChanged(nameof(EmployeeStatusLabel));
            }
        }

        public string EmployeeStatusLabel
        {
            get 
            { 
                if (SelectedCustomer == null)
                {
                    return string.Empty;
                }
                return SelectedCustomer.IsActive ? "Active" : "Left UPS"; 
            }
        }


        public MainViewModel()
        {
            Customers = new ObservableCollection<Customer>();
        }

        public ICommand LoadCommand => new ActionCommand<object>(Load);

        public ICommand ShowUserInformationDialogCommand 
            => new ActionCommand<object>(ShowUserInformationDialogExecute);

        public ICommand ShowGoodsAttributesDialogCommand
            => new ActionCommand<object>(ShowGoodsAttributesDialogExecute);

        private void Load(object obj)
        {
            if (!File.Exists("Data\\Customers.json"))
            {
                return;
            }
            var text = File.ReadAllText("Data\\Customers.json");
            var customers = JsonConvert.DeserializeObject<List<Customer>>(text);
            Customers = new ObservableCollection<Customer>(customers);
            var selectedCustomer = Customers.FirstOrDefault();
            selectedIndex = Customers.IndexOf(selectedCustomer);
            OnPropertyChanged(nameof(Customers));
            OnPropertyChanged(nameof(SelectedCustomer));
            OnPropertyChanged(nameof(EmployeeStatusLabel));
        }

        private static void ShowUserInformationDialogExecute(object obj)
        {
            var viewModel = new UserInformationViewModel();
            var view = new UserInformationDialog
            {
                DataContext = viewModel
            };
            view.ShowDialog();
        }

        private static void ShowGoodsAttributesDialogExecute(object obj)
        {
            var viewModel = new GoodsAttributesViewModel();
            var view = new GoodsAttributesDialog()
            {
                DataContext = viewModel
            };
            view.ShowDialog();
        }

        private int selectedIndex = -1;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}