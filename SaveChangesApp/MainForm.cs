using SaveChanges;
using System.Windows.Forms.Integration;

namespace SaveChangesApp
{
    public partial class MainForm : Form
    {
        public event SaveCustomerDelegate? SaveCustomer;
        public delegate void SaveCustomerDelegate();


        private ElementHost? customerHost;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            customerHost = new ElementHost();
            customerHost.Dock = DockStyle.Fill;
            wpfHostPanel.Controls.Add(customerHost);
            var customerViewModel = new CustomerViewModel();
            var customerView = new CustomerView();
            customerView.DataContext = customerViewModel;
            customerHost.Child = customerView;
            SaveCustomer += customerView.OnSave;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
        }

        private void SaveToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            SaveCustomer?.Invoke();
        }

    }
}