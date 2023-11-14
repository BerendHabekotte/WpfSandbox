using CustomerControls;
using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CustomerApp
{
    public partial class MainForm : Form
    {
        public event SaveCustomerDelegate SaveCustomer;
        public delegate void SaveCustomerDelegate();

        private ElementHost customerHost;

        public MainForm()
        {
            InitializeComponent();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCustomer?.Invoke();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            customerHost = new ElementHost
            {
                Dock = DockStyle.Fill
            };
            wpfHostPanel.Controls.Add(customerHost);
            var customerViewModel = new CustomerViewModel();
            var customerView = new CustomerView
            {
                DataContext = customerViewModel
            };
            customerHost.Child = customerView;
            //            SaveCustomer += customerView.OnSave;
            //var selectedItemComboboxViewModel = new SelectedItemComboboxViewModel();
            //var selectedItemComboboxView = new SelectedItemComboboxView()
            //{
            //    DataContext = selectedItemComboboxViewModel
            //};
            //customerHost.Child = selectedItemComboboxView;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
        }


        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void wpfHostPanel_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveCustomer?.Invoke();
        }
    }
}
