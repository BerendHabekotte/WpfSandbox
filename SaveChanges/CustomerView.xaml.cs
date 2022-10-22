using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SaveChanges
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView
    {
        public CustomerView()
        {
            InitializeComponent();
        }

        private void SaveVisual(DependencyObject visual, CustomerViewModel viewModel)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                // Retrieve child visual at specified index value.
                var childVisual = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (childVisual is UIElement { IsFocused: true } uiElement)
                {
                    SaveFocused(uiElement, viewModel);
                }
                // Enumerate children of the child visual object.
                SaveVisual(childVisual, viewModel);
            }
        }

        private void SaveFocused(UIElement element, CustomerViewModel viewModel)
        {
            switch (element)
            {
                case TextBox textBox:
                    if (textBox.IsFocused)
                        viewModel.LostFocusCommand.Execute(new[] { textBox.Name, textBox.Text });
                    break;
                case ComboBox comboBox:
                    viewModel.LostFocusCommand.Execute(new[] { comboBox.Name, comboBox.Text });
                    break;
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
