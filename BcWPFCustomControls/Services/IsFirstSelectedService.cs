using System.Data;
using System.Linq;
using System.Windows;
using BcWPFCustomControls.Controls;

namespace BcWPFCustomControls.Services
{
    public static class IsFirstSelectedService
    {
        public static readonly DependencyProperty IsFirstSelectedProperty = DependencyProperty.RegisterAttached(
            "IsFirstSelected",
            typeof(bool),
            typeof(IsFirstSelectedService),
            new PropertyMetadata(false, OnIsFirstSelectedChanged)
            );

        public static bool GetIsFirstSelected(ReferenceComboBox c)
        {
            return (bool)c.GetValue(IsFirstSelectedProperty);
        }

        public static void SetIsFirstSelected(ReferenceComboBox c, bool value)
        {
            c.SetValue(IsFirstSelectedProperty, value);
        }


        private static void OnIsFirstSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ReferenceComboBox comboBox))
            {
                return;
            }
            comboBox.KeyUp += ComboBoxKeyUp;
        }

        private static void ComboBoxKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var comboBox = (ReferenceComboBox)sender;
            comboBox.SelectedIndex = comboBox.ItemsSource.Cast<DataRowView>().Any() ? 0 : -1;
        }
    }
}