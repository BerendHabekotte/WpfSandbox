using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaveChanges
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
        }

        private void SaveVisual(Visual visual, CustomerViewModel customerViewModel)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(visual, i);

                if (childVisual is TextBox)
                {
                    var textBox = (TextBox)childVisual;
                    customerViewModel.LostFocusCommand.Execute(new string[] { textBox.Name, textBox.Text} );
                }

                // Enumerate children of the child visual object.
                SaveVisual(childVisual, customerViewModel);
            }
        }

        public void OnSave()
        {
            if (DataContext is not CustomerViewModel customerViewModel)
                return;
            SaveVisual(this, customerViewModel);
        }
    }
}
