using BcWpfCommon.Commands;
using BOSSAutoRef.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class ReferenceComboBox : ComboBox, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static ReferenceComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ReferenceComboBox),
                new FrameworkPropertyMetadata(typeof(ReferenceComboBox)));
            SelectedValueProperty.OverrideMetadata(
                typeof(ComboBox),
                new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.LostFocus,
                });
        }

        public ReferenceComboBox()
        {
            Loaded += ReferenceComboBox_Loaded;
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
                typeof(ReferenceComboBox),
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
                typeof(ReferenceComboBox),
                new PropertyMetadata());

        public static readonly DependencyProperty AutoRefTypeProperty =
            DependencyProperty.Register(
                nameof(AutoRefType),
                typeof(AutoRefTypes),
                typeof(ReferenceComboBox),
                new PropertyMetadata(AutoRefTypes.NONE));

        public AutoRefTypes AutoRefType
        {
            get => (AutoRefTypes)GetValue(AutoRefTypeProperty);
            set => SetValue(AutoRefTypeProperty, value);
        }

        public static readonly DependencyProperty AutoRefFilterProperty =
            DependencyProperty.Register(
                nameof(AutoRefFilter),
                typeof(string),
                typeof(ReferenceComboBox),
                new PropertyMetadata(string.Empty, null));

        public string AutoRefFilter
        {
            get => (string)GetValue(AutoRefFilterProperty);
            set => SetValue(AutoRefFilterProperty, value);
        }

        public string CustomDescriptionSeparator
        {
            get => (string)GetValue(CustomDescriptionSeparatorProperty);
            set => SetValue(CustomDescriptionSeparatorProperty, value);
        }

        public static readonly DependencyProperty CustomDescriptionSeparatorProperty =
            DependencyProperty.Register(
                nameof(CustomDescriptionSeparator),
                typeof(string),
                typeof(ReferenceComboBox),
                new PropertyMetadata(" "));

        public string CustomDescriptionFields
        {
            get => (string)GetValue(CustomDescriptionFieldsProperty);
            set => SetValue(CustomDescriptionFieldsProperty, value);
        }

        public static readonly DependencyProperty CustomDescriptionFieldsProperty =
            DependencyProperty.Register(
                nameof(CustomDescriptionFields),
                typeof(string),
                typeof(ReferenceComboBox),
                new PropertyMetadata(string.Empty));

        public string SelectedValueMemberPath
        {
            get => (string)GetValue(SelectedValueMemberPathProperty);
            set => SetValue(SelectedValueMemberPathProperty, value);
        }

        public static readonly DependencyProperty SelectedValueMemberPathProperty =
            DependencyProperty.Register(
                nameof(SelectedValueMemberPath),
                typeof(string),
                typeof(ReferenceComboBox),
                new PropertyMetadata(string.Empty));


        private void ReferenceComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            table = CreateDataTable();
            CreateCustomDescriptionColumn();
            itemsSource = ItemsSource = table.AsDataView();
            GotFocus += ReferenceComboBox_GotFocus;
            LostFocus += ReferenceComboBox_LostFocus;
            KeyDown += ReferenceComboBox_KeyDown;
            KeyUp += ReferenceComboBox_KeyUp;
            SelectionChanged += ReferenceComboBox_SelectionChanged;
            if (SelectedValueMemberPath != null)
            {
                TextSearch.SetTextPath(this, SelectedValueMemberPath);
                SetSelectedValueMemberPath();
            }

        }

        private DataTable CreateDataTable()
        {
            try
            {
                if (AutoRefType == AutoRefTypes.NONE)
                {
                    switch (ItemsSource)
                    {
                        case DataView _:
                            return ((DataView)ItemsSource).ToTable();
                        default:
                            return new DataTable();
                    }
                }
                var dataView = Builder
                    .GetInstance()
                    .GetRefDataView(AutoRefType, string.Empty, AutoRefFilter, string.Empty, false);
                return dataView.ToTable();
            }
            catch
            {
                return new DataTable();
            }
        }

        private void ReferenceComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IsReadOnly)
            {
                return;
            }

            if (SelectedValueMemberPath == null)
            {
                return;
            }
            TextSearch.SetTextPath(this, null);
            Text = GetMember(DisplayMemberPath);
            switch (e.OriginalSource)
            {
                case ComboBox box:
                    (box.SelectionBoxItem as TextBox)?.SelectAll();
                    break;
                case TextBox box:
                    box.SelectAll();
                    break;
            }
        }

        private void ReferenceComboBox_LostFocus(object sender, RoutedEventArgs e)
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
                SearchText = string.Empty;
                SelectedValue = string.Empty;
                var bindingExpression = GetBindingExpression(SelectedValueProperty);
                if (bindingExpression == null)
                {
                    SearchText = string.Empty;
                    return;
                }
                bindingExpression.UpdateSource();
            }
            if (!string.IsNullOrEmpty(SelectedValueMemberPath))
            {
                TextSearch.SetTextPath(this, SelectedValueMemberPath);
            }
            SetSelectedValueMemberPath();
            SearchText = string.Empty;
        }

        private void ReferenceComboBox_KeyDown(object sender, KeyEventArgs e)
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

        private void ReferenceComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.OriginalSource is TextBox item))
            {
                return;
            }
            if (!e.Key.Equals(Key.Enter))
            {
                return;
            }
            SearchText = item.Text;
            var count = ItemsSource.OfType<DataRowView>().Count();
            if (count == 1)
            {
                SelectedIndex = 0;
            }
            else
            {
                IsDropDownOpen = true;
            }
        }

        private void ReferenceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HasEffectiveKeyboardFocus)
            {
                return;
            }
            SetSelectedValueMemberPath();
            e.Handled = true;
        }

        private void SetSelectedValueMemberPath()
        {
            if (string.IsNullOrEmpty(SelectedValueMemberPath))
            {
                return;
            }
            Text = GetMember(SelectedValueMemberPath);
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

        private void CreateCustomDescriptionColumn()
        {
            if (!table.Columns.Contains(CustomDescriptionName))
            {
                table.Columns.Add(CustomDescriptionName, typeof(string));
            }
            var customDescriptionFields = CreateCustomDescriptionFields();
            foreach (DataRow row in table.Rows)
            {
                FillCustomDescriptionColumn(row, customDescriptionFields);
            }
        }

        private string[] CreateCustomDescriptionFields()
        {
            if (string.IsNullOrEmpty(CustomDescriptionFields))
            {
                return CreateDefaultCustomDescriptionFields();
            }
            var result = CreateSpecificCustomDescriptionFields();
            if (result.Length == 0)
            {
                return CreateDefaultCustomDescriptionFields();
            }
            return result;
        }

        private string[] CreateDefaultCustomDescriptionFields()
        {
            var fields = new List<string>
            {
                "Code",
                "Description"
            };
            if (table.Columns.Contains("OriginalDescription"))
            {
                var areEqual = true;
                foreach (DataRow row in table.Rows)
                {
                    areEqual = row["Description"].ToString().Equals(row["OriginalDescription"].ToString());
                    if (!areEqual)
                    {
                        break;
                    }
                }
                if (!areEqual)
                {
                    fields.Add("OriginalDescription");
                }
            }
            return fields.ToArray();
        }

        private string[] CreateSpecificCustomDescriptionFields()
        {
            var rawFields = CustomDescriptionFields.Split('|');
            var fields = new List<string>();
            foreach (var field in rawFields)
            {
                if (table.Columns.Contains(field))
                {
                    fields.Add(field);
                }
            }
            return fields.ToArray();
        }

        private void FillCustomDescriptionColumn(DataRow row, string[] customDescriptionFields)
        {
            var builder = new StringBuilder();
            foreach (var field in customDescriptionFields)
            {
                builder.Append(row[field]);
                if (!field.Equals(customDescriptionFields[customDescriptionFields.Length - 1]))
                {
                    builder.Append(CustomDescriptionSeparator);
                }
            }
            row["CustomDescription"] = builder.ToString();
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
            ItemsSource = string.IsNullOrEmpty(SearchText) ? itemsSource : GetFilteredItemSource();
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

        private const string CustomDescriptionName = "CustomDescription";

        private IEnumerable itemsSource;
        private DataTable table;
    }
}
