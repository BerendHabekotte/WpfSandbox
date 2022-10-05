using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomerControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            Loaded += CustomerView_Loaded;
        }

        private void CustomerView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LostFocus += CustomerView_LostFocus;
            LostMouseCapture += CustomerView_LostMouseCapture;
            MouseLeave += CustomerView_MouseLeave;
        }

        private void CustomerView_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource is UIElement)
            {
                return;
            }
            SaveVisual(this);
        }

        private void CustomerView_LostMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource is UIElement)
            {
                return;
            }
            SaveVisual(this);
        }

        private void CustomerView_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (e.OriginalSource is UIElement)
            {
                return;
            }
            SaveVisual(this);
        }

        public void OnSave()
        {
            SaveVisual(this);
        }

        private void SaveVisual(Visual visual)
        {
            if (TryToChangeFocus(visual))
            {
                return;
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(visual, i);
                var focusChanged = TryToChangeFocus(childVisual);
                if (focusChanged)
                {
                    return;
                }
                SaveVisual(childVisual);
            }
        }

        private bool TryToChangeFocus(Visual visual)
        {
            if (visual is Control control)
            {
                if (control.IsFocused)
                {
                    return false;
                }
                if (control.Focusable)
                {
                    return control.Focus();
                }
            }
            return false;
        }
    }
}
