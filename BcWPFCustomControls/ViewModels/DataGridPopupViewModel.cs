using Prism.Commands;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BcWPFCustomControls.ViewModels
{
    internal class DataGridPopupViewModel : INotifyPropertyChanged
    {
        private IEnumerable items;
        private string selectedValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable ItemsSource
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged(nameof(ItemsSource));
            }
        }

        public string SelectedValue
        {
            get => selectedValue;
            set
            {
                selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
                ((DelegateCommand<Window>)OkCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand DoubleClickCommand { get; private set; }

        public ICommand OkCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        public DataGridPopupViewModel()
        {
            OkCommand = new DelegateCommand<Window>(OnOkExecute, OnOkCanExecute);
            DoubleClickCommand = new DelegateCommand<Window>(OnOkExecute);
            CloseCommand = new DelegateCommand<Window>(OnCloseExecute);
        }

        private bool OnOkCanExecute(Window arg)
            => selectedValue != null;

        private void OnOkExecute(Window window)
        {
            if (window == null)
                return;
            window.DialogResult = true;
            (window).Close();
        }

        private void OnCloseExecute(Window window)
        {
            if (window == null)
                return;
            window.DialogResult = false;
            (window).Close();
        }

        // TODO: move it to base class
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}