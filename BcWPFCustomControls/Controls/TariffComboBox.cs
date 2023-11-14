using BcWpfCommon.Commands;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class TariffComboBox : ComboBox, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private const int MinimumDropDownLength = 4;

        static TariffComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TariffComboBox),
                new FrameworkPropertyMetadata(typeof(TariffComboBox)));
            SelectedValueProperty.OverrideMetadata(
                typeof(TariffComboBox),
                new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.LostFocus,
                });
        }

        public TariffComboBox()
        {
            Loaded += TariffComboBox_Loaded;
        }

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                nameof(PlaceholderText),
                typeof(string),
                typeof(TariffComboBox),
                new PropertyMetadata());

        public HorizontalAlignment WatermarkHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(WatermarkHorizontalAlignmentProperty);
            set => SetValue(WatermarkHorizontalAlignmentProperty, value);
        }

        public static readonly DependencyProperty WatermarkHorizontalAlignmentProperty =
            DependencyProperty.Register(
                nameof(WatermarkHorizontalAlignment),
                typeof(HorizontalAlignment),
                typeof(TariffComboBox),
                new PropertyMetadata());

        private void TariffComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            DropDownOpened += TariffComboBox_DropDownOpened;
            GotFocus += TariffComboBox_GotFocus;
            LostFocus += TariffComboBox_LostFocus;
            KeyDown += TariffComboBox_KeyDown;
            KeyUp += TariffComboBox_KeyUp;
            SelectionChanged += TariffComboBox_SelectionChanged;
            itemsSource = ItemsSource;
        }

        private void TariffComboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (!(sender is ComboBox combobox))
            {
                return;
            }
            var textBox = (TextBox)combobox.Template.FindName("PART_EditableTextBox", combobox);
            textBox.Select(textBox.Text.Length, 0);
        }

        private void TariffComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void TariffComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsReadOnly)
            {
                return;
            }

            if (!(e.OriginalSource is TextBox item))
            {
                return;
            }

            IsDropDownOpen = false;
            if (SelectedIndex == -1 || item.Text != (SelectedItem as DataRowView)?[DisplayMemberPath] as string)
            {
                item.Text = string.Empty;
                SelectedValue = string.Empty;
                var bindingExpression = GetBindingExpression(SelectedValueProperty);
                if (bindingExpression == null)
                {
                    SearchText = string.Empty;
                    return;
                }
                bindingExpression.UpdateSource();
            }
            searchText = string.Empty;
            ItemsSource = itemsSource;
        }

        private void TariffComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
            {
                return;
            }
            if (e.Key == Key.Tab)
            {
                if (!(e.OriginalSource is TextBox item))
                {
                    return;
                }
                if (string.IsNullOrEmpty(item.Text))
                {
                    return;
                }
                if (SelectedIndex == -1 || item.Text != GetMember(DisplayMemberPath))
                {
                    SetSelectedIndexOnTabOut(item.Text);
                }
                return;
            }
            if (e.Key != Key.Escape)
            {
                return;
            }
            var bindingExpression = GetBindingExpression(SelectedValueProperty);
            bindingExpression?.UpdateTarget();
        }

        private void TariffComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.OriginalSource is TextBox item))
            {
                e.Handled = false;
                return;
            }

            if (e.Key.Equals(Key.Up) 
                || e.Key.Equals(Key.Down) 
                || e.Key.Equals(Key.Left) 
                || e.Key.Equals(Key.Right) 
                || e.Key.Equals(Key.Enter)
                || e.Key.Equals(Key.PageDown)
                || e.Key.Equals(Key.PageUp)
                || e.Key.Equals(Key.Insert)
                || e.Key.Equals(Key.Delete)
                || e.Key.Equals(Key.Escape))
            {
                e.Handled = false;
                return;
            }
            SearchText = item.Text;
            if (string.IsNullOrEmpty(Text))
            {
                IsDropDownOpen = false;
                return;
            }
            IsDropDownOpen = Text.Length >= MinimumDropDownLength;
        }

        private void TariffComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private string GetMember(string path)
        {
            if (SelectedIndex == -1)
            {
                return string.Empty;
            }
            string result = ItemsSource
                .OfType<DataRowView>()
                .ToList()[SelectedIndex][path]
                .ToString();
            return result;
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText == value)
                {
                    return;
                }
                searchText = value;
                SelectedIndex = -1;
                FilterItemsSource();
                Text = searchText;
            }
        }

        private void FilterItemsSource()
        {
            ItemsSource = string.IsNullOrEmpty(SearchText) 
                ? itemsSource 
                : SearchText.Length < MinimumDropDownLength
                    ? itemsSource
                    : GetFilteredItemSource();
        }

        private IEnumerable GetFilteredItemSource()
        {
            foreach (DataRowView row in itemsSource)
            {
                if (row[DisplayMemberPath]
                    .ToString()
                    .StartsWith(SearchText))
                {
                    yield return row;
                }
            }
        }

        private void SetSelectedIndexOnTabOut(string value)
        {
            SearchText = value;
            var count = ItemsSource.OfType<DataRowView>().Count();
            if (count == 1)
            {
                SelectedIndex = 0;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private UserControl popUpContent;

        public UserControl PopUpContent
        {
            get => popUpContent;
            set
            {
                popUpContent = value;
                OnPropertyChanged();
            }
        }

        private bool isPopupOpen;

        public bool IsPopupOpen
        {
            get => isPopupOpen;
            set
            {
                isPopupOpen = value;
                OnPropertyChanged();
            }
        }

        private string popUpGridSelectedValue;

        public string PopUpGridSelectedValue
        {
            get => popUpGridSelectedValue;
            set
            {
                popUpGridSelectedValue = value;
                OnPropertyChanged();
            }
        }

        public ICommand ComboBoxMouseDoubleClick => new ActionCommand<object>(ComboBoxMouseDoubleClickAction, param => true);
        public ICommand PopUpGridSelectedItemDoubleClick => new ActionCommand<object>(PopUpGridSelectedItemDoubleClickAction, param => true);
        public ICommand ClosePopup => new ActionCommand<object>(ClosePopupClickAction, param => true);

        private void ClosePopupClickAction(object obj)
        {
            IsPopupOpen = false;
        }

        private void ComboBoxMouseDoubleClickAction(object obj)
        {
            if (IsReadOnly)
                return;
            var dialog = new DataGridPopup(ItemsSource);
            var dialogResult = dialog.ShowDialog() ?? false;
            if (dialogResult)
            {
                SelectedValue = dialog.SelectedValue;
            }
        }

        private void PopUpGridSelectedItemDoubleClickAction(object obj)
        {
            SelectedValue = PopUpGridSelectedValue;
            IsPopupOpen = false;
        }

        private IEnumerable itemsSource;

    }
}