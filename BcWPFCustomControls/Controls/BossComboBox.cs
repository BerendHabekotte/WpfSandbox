using BOSSAutoRef.Factory;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class BossComboBox: ComboBox
    {
        static BossComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BossComboBox), 
                new FrameworkPropertyMetadata(typeof(BossComboBox)));
        }

        public BossComboBox()
        {
            Loaded += BossComboBox_Loaded;
        }

        public static readonly DependencyProperty AutoRefTypeProperty =
            DependencyProperty.Register(
                "AutoRefType",
                typeof(AutoRefTypes),
                typeof(BossComboBox),
                new PropertyMetadata(AutoRefTypes.NONE, null));

        public AutoRefTypes AutoRefType
        {
            get => (AutoRefTypes)GetValue(AutoRefTypeProperty);
            set
            {
                SetValue(AutoRefTypeProperty, value);
            }
        }

        public static readonly DependencyProperty AutoRefFilterProperty =
            DependencyProperty.Register(
                "AutoRefFilter",
                typeof(string),
                typeof(BossComboBox),
                new PropertyMetadata("", null));

        public string AutoRefFilter
        {
            get => (string)GetValue(AutoRefFilterProperty);
            set
            {
                SetValue(AutoRefFilterProperty, value);
            }
        }

        private void BossComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (AutoRefType == AutoRefTypes.NONE)
                return;

            if (AutoRefType != AutoRefTypes.TYPE_BrokStatusValue)
                return;
            if (AutoRefFilter != "CT")
            {
                return;
            }
            var dataView = Builder.GetInstance().GetRefDataView(AutoRefType, "", AutoRefFilter, "", false);
            AddCustomDescriptionColumn(dataView);
            foreach (DataRow row in dataView.Table.Rows)
            {
                FillCustomDescriptionField(row);
            }

            PreviewLostKeyboardFocus += BossComboBox_PreviewLostKeyboardFocus;
            LostFocus += BossComboBox_LostFocus;
            KeyDown += BossComboBox_KeyDown;
            KeyUp += BossComboBox_KeyUp;
            try
            {
                itemsSource = ItemsSource = dataView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCustomDescriptionColumn(DataView itemsSource)
        {
            if (!itemsSource.Table.Columns.Contains("CustomDescription"))
                itemsSource.Table.Columns.Add("CustomDescription", typeof(string));
        }

        private void FillCustomDescriptionField(DataRow row)
        {
            var builder = new StringBuilder();
            builder.Append(row["Code"].ToString());
            builder.Append(" ");
            builder.Append(row["Description"].ToString());
            if (row.Table.Columns.Contains("OriginalDescription"))
            {
                if (!row["Description"].ToString().Equals(row["OriginalDescription"].ToString()))
                {
                    builder.Append(" ");
                    builder.Append(row["OriginalDescription"].ToString());
                }
            }
            row["CustomDescription"] = builder.ToString();
        }

        private void BossComboBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsReadOnly)
            {
                return;
            }
            if (!(e.OldFocus is TextBox item))
            {
                return;
            }
            IsDropDownOpen = true;
            ValidateStatus(e, item);
        }

        private void BossComboBox_KeyDown(object sender, KeyEventArgs e)
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
                ValidateStatus(e, item);
                return;
            }
            if (e.Key != Key.Escape)
            {
                return;
            }
            var bindingExpression = GetBindingExpression(SelectedValueProperty);
            if (bindingExpression == null)
            {
                return;
            }
            bindingExpression.UpdateTarget();
        }

        private void BossComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.OriginalSource is TextBox item))
            {
                return;
            }
            if ((e.Key == Key.Up) || (e.Key == Key.Down) || (e.Key == Key.Left) || (e.Key == Key.Right))
            {
                return;
            }
            if (!e.Key.Equals(Key.Enter))
            {
                return;
            }
            SearchText = item.Text;
            IsDropDownOpen = true;
            ValidateStatus(e, item);
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                FilterItemsSource();
            }
        }

        private void FilterItemsSource()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                ItemsSource = itemsSource;
            }
            else
            {
                ItemsSource = GetFilteredItemSource();
            }
        }

        private IEnumerable GetFilteredItemSource()
        {
            foreach (DataRowView row in itemsSource)
            {
                if (row[DisplayMemberPath]
                    .ToString()
                    .IndexOf(SearchText, StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    yield return row;
                }
            }
        }

        private void BossComboBox_LostFocus(object sender, RoutedEventArgs e)
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
            if (SelectedIndex == -1 || item.Text != (SelectedItem as DataRowView)[DisplayMemberPath] as string)
            {
                item.Text = string.Empty;
                SelectedValue = string.Empty;
                var bindingExpression = GetBindingExpression(SelectedValueProperty);
                if (bindingExpression == null)
                {
                    return;
                }
                bindingExpression.UpdateSource();
            }
            ValidateStatus(e, item);
        }



        private void ValidateStatus(RoutedEventArgs e, TextBox textBox)
        {
            if (textBox.Name != "PART_EditableTextBox")
            {
                return;
            }
            if (!Validation.GetHasError(this))
            {
                return;
            }
            e.Handled = true;
        }

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(BossComboBox),
                new PropertyMetadata(new PropertyChangedCallback(PlaceholderTextChanged)));

        private static void PlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public HorizontalAlignment WatermarkHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(WatermarkHorizontalAlignmentProperty); }
            set { SetValue(WatermarkHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty WatermarkHorizontalAlignmentProperty =
            DependencyProperty.Register(
                "WatermarkHorizontalAlignment",
                typeof(HorizontalAlignment),
                typeof(BossComboBox),
                new PropertyMetadata(new PropertyChangedCallback(WatermarkHorizontalAlignmentChanged)));

        private static void WatermarkHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private IEnumerable itemsSource;
    }
}
