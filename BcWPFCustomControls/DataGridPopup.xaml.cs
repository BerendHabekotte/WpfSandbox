using BcWPFCustomControls.ViewModels;
using System.Collections;
using System.Windows;

namespace BcWPFCustomControls
{
    /// <summary>
    /// Interaction logic for DataGridPopup.xaml
    /// </summary>
    public partial class DataGridPopup : Window
    {
        private readonly DataGridPopupViewModel viewModel = new DataGridPopupViewModel();

        public string SelectedValue
            => viewModel.SelectedValue;

        public DataGridPopup(IEnumerable items)
        {
            InitializeComponent();

            viewModel.ItemsSource = items;
            DataContext = viewModel;
        }
    }
}