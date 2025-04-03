using BcWpfCommon.Commands;
using DataGridTestApplication.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;

namespace DataGridTestApplication.ViewModels
{
    public class UserInformationViewModel : INotifyPropertyChanged
    {
        public UserInformationViewModel()
        {
            LoadContacts();
        }

        public ObservableCollection<UserOrganizationPersonalContactModel> Contacts
        {
            get; private set;
        }

        public ICommand AddContactCommand => new ActionCommand<object>(AddContactCommandExecute);

        public ICommand AddCommand => new ActionCommand<object>(AddCommandExecute);

        public ICommand DeleteCommand => new ActionCommand<object>(DeleteCommandExecute);

        public ICommand ConfirmCommand => new ActionCommand<object>(ConfirmCommandExecute);

        private UserOrganizationPersonalContactModel selectedContact;
        public UserOrganizationPersonalContactModel SelectedContact
        {
            get => selectedContact;
            set
            {
                if (selectedContact == value)
                {
                    return;
                }
                selectedContact = value;
                if (selectedContact != null)
                {
                    if (ContactName != selectedContact.Name)
                    {
                        contactName = selectedContact.Name;
                    }
                    if (ContactTelephone != selectedContact.Telephone)
                    {
                        contactTelephone = selectedContact.Telephone;
                    }
                }
                OnPropertyChanged();
            }
        }

        private string contactName;
        public string ContactName
        {
            get => contactName;
            set
            {
                contactName = value;
                if (selectedContact != null)
                {
                    selectedContact.Name = contactName;
                }
                OnPropertyChanged();
            }
        }

        private string contactTelephone;
        public string ContactTelephone
        {
            get => contactTelephone;
            set
            {
                contactTelephone = value;
                if (selectedContact != null)
                {
                    selectedContact.Telephone = contactTelephone;
                }
                OnPropertyChanged();
            }
        }

        private bool areAllSelected;
        public bool AreAllSelected
        {
            get => areAllSelected;
            set
            {
                areAllSelected = value;
                foreach (var contact in Contacts)
                {
                    contact.PropertyChanged -= ContactPropertyChanged;
                    contact.IsSelected = areAllSelected;
                    contact.PropertyChanged += ContactPropertyChanged;
                }
                OnPropertyChanged();
            }
        }

        private void ContactPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            areAllSelected = Contacts.All(c => c.IsSelected);
            OnPropertyChanged();
        }

        private void LoadContacts()
        {
            Contacts = new ObservableCollection<UserOrganizationPersonalContactModel>();
        }

        private static List<UserOrganizationPersonalContactModel> GetContacts(string[] names, string[] phoneNumbers)
        {
            if ((names.Length.Equals(1) && string.IsNullOrEmpty(names[0])) || 
                !names.Length.Equals(phoneNumbers.Length))
            {
                return new List<UserOrganizationPersonalContactModel>();
            }
            return names.Select((t, index) => new UserOrganizationPersonalContactModel
            {
                IsSelected = false,
                Index = index + 1,
                Name = t,
                Telephone = phoneNumbers[index],
            }).ToList();
        }

        private void AddContactCommandExecute(object obj)
        {
            if (selectedContact != null)
            {
                return;
            }
            var contact = new UserOrganizationPersonalContactModel
            {
                Index = Contacts.Count + 1,
                IsSelected = false,
                Name = ContactName,
                Telephone = ContactTelephone
            };
            contact.PropertyChanged += ContactPropertyChanged;
            Contacts.Add(contact);
            SelectedContact = contact;
            areAllSelected = false;
            OnPropertyChanged();
        }

        private void AddCommandExecute(object obj)
        {
            SelectedContact = null;
            ContactName = string.Empty;
            ContactTelephone = string.Empty;
        }

        private void DeleteCommandExecute(object obj)
        {
            SelectedContact = null;
            var contacts = new List<UserOrganizationPersonalContactModel>();
            foreach (var contact in Contacts)
            {
                if (contact.IsSelected)
                {
                    continue;
                }
                contacts.Add(contact);
                contact.Index = contacts.Count;
            }
            Contacts = new ObservableCollection<UserOrganizationPersonalContactModel>(contacts);
            ContactName = string.Empty;
            ContactTelephone = string.Empty;
            AreAllSelected = false;
            OnPropertyChanged();
        }

        private static void ConfirmCommandExecute(object obj)
        {
            if (!(obj is Window dialog))
            {
                return;
            }
            dialog.DialogResult = true;
            dialog.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}