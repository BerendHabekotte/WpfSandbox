using BcWpfCommon.Commands;
using BcWPFCustomControls.Controls.Enums;
using BOSSAutoRef.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
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

        private const string CustomDescriptionName = "CustomDescription";
        private IEnumerable originalItemsSource;
        private DataTable table;
        private IEnumerable<string> customCodes;
        private int oldSelectedIndex;
        private bool hasCustomCodes;

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

        public AutoRefTypes AutoRefType
        {
            get => (AutoRefTypes)GetValue(AutoRefTypeProperty);
            set => SetValue(AutoRefTypeProperty, value);
        }

        public static readonly DependencyProperty AutoRefTypeProperty =
            DependencyProperty.Register(
                nameof(AutoRefType),
                typeof(AutoRefTypes),
                typeof(ReferenceComboBox),
                new PropertyMetadata(AutoRefTypes.NONE));

        public string AutoRefFilter
        {
            get => (string)GetValue(AutoRefFilterProperty);
            set => SetValue(AutoRefFilterProperty, value);
        }

        public static readonly DependencyProperty AutoRefFilterProperty =
            DependencyProperty.Register(
                nameof(AutoRefFilter),
                typeof(string),
                typeof(ReferenceComboBox),
                new PropertyMetadata(string.Empty, null));

        public string LookupFilter
        {
            get => (string)GetValue(LookupFilterProperty);
            set => SetValue(LookupFilterProperty, value);
        }

        public static readonly DependencyProperty LookupFilterProperty =
            DependencyProperty.Register(
                nameof(LookupFilter),
                typeof(string),
                typeof(ReferenceComboBox),
                new PropertyMetadata(string.Empty, null));

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

        public bool OpenDropDownOnEnterKeyOnly
        {
            get => (bool)GetValue(OpenDropDownOnEnterKeyOnlyProperty);
            set => SetValue(OpenDropDownOnEnterKeyOnlyProperty, value);
        }

        public static readonly DependencyProperty OpenDropDownOnEnterKeyOnlyProperty =
            DependencyProperty.Register(
                nameof(OpenDropDownOnEnterKeyOnly),
                typeof(bool),
                typeof(ReferenceComboBox),
                new PropertyMetadata(true));

        public FilterMethods FilterMethod
        {
            get => (FilterMethods)GetValue(FilterMethodProperty);
            set => SetValue(FilterMethodProperty, value);
        }

        public static readonly DependencyProperty FilterMethodProperty =
            DependencyProperty.Register(
                nameof(FilterMethod),
                typeof(FilterMethods),
                typeof(ReferenceComboBox),
                new PropertyMetadata(FilterMethods.Contains));

        private void ReferenceComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            oldSelectedIndex = -1;
            customCodes = GetCustomCodes();
            hasCustomCodes = !customCodes.Count().Equals(0);
            table = CreateDataTable();
            CreateCustomDescriptionColumn();
            SetItemsSource();
            GotFocus += ReferenceComboBox_GotFocus;
            LostFocus += ReferenceComboBox_LostFocus;
            KeyUp += ReferenceComboBox_KeyUp;
            SelectionChanged += ReferenceComboBox_SelectionChanged;
            DropDownOpened += ReferenceComboBox_DropDownOpened;
            DropDownClosed += ReferenceComboBox_DropDownClosed;
            if (SelectedValueMemberPath == null)
            {
                return;
            }
            TextSearch.SetTextPath(this, SelectedValueMemberPath);
            SetSelectedValueMemberPath();
        }

        private IEnumerable<string> GetCustomCodes()
        {
            try
            {
                if (string.IsNullOrEmpty(LookupFilter))
                {
                    return new string[] { };
                }
                var lookupView = Builder.GetInstance().GetRefDataView(
                    AutoRefTypes.TYPE_LocalLookupCodes,
                    LookupFilter,
                    "LPM",
                    string.Empty,
                    false);
                if (lookupView == null)
                {
                    return new string[] { };
                }
                return !lookupView.Count.Equals(1)
                    ? new string[] { }
                    : lookupView[0].Row["Description"].ToString().Split('|');
            }
            catch
            {
                return new string[] { };
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
                        case DataView view: return view.ToTable();
                        default: return new DataTable();
                    }
                }
                var dataView = Builder
                    .GetInstance()
                    .GetRefDataView(
                        AutoRefType,
                        string.Empty,
                        AutoRefFilter,
                        string.Empty,
                        false);
                return dataView.ToTable();
            }
            catch
            {
                return new DataTable();
            }
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
            return result.Length == 0 ? CreateDefaultCustomDescriptionFields() : result;
        }

        private string[] CreateDefaultCustomDescriptionFields()
        {
            var fields = new List<string>
            {
                "Code",
                "Description"
            };
            if (!table.Columns.Contains("OriginalDescription"))
            {
                return fields.ToArray();
            }
            if (table.Rows.Cast<DataRow>()
                .Any(row => !row["Description"].ToString().Equals(row["OriginalDescription"].ToString())))
            {
                fields.Add("OriginalDescription");
            }
            return fields.ToArray();
        }

        private string[] CreateSpecificCustomDescriptionFields()
        {
            var rawFields = CustomDescriptionFields.Split('|');
            return rawFields.Where(field => table.Columns.Contains(field)).ToArray();
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

        private void SetItemsSource()
        {
            if (ItemsSource != null)
            {
                oldSelectedIndex = SelectedIndex;
                ItemsSource = table.AsDataView();
                SelectedIndex = oldSelectedIndex;
            }
            else
            {
                ItemsSource = table.AsDataView();
            }
            originalItemsSource = ItemsSource;
        }

        private void SetSelectedValueMemberPath()
        {
            if (string.IsNullOrEmpty(SelectedValueMemberPath))
            {
                return;
            }
            Text = GetMember(SelectedValueMemberPath);
            Trace.WriteLine($"{DateTime.Now} - SetSelectedValueMemberPath: Text: {Text}.");
        }

        private string GetMember(string path)
        {
            if (SelectedIndex == -1)
            {
                return string.Empty;
            }
            var result = ItemsSource
                .OfType<DataRowView>()
                .ToList()[SelectedIndex][path]
                .ToString();
            return result;
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
            if (SelectedIndex != -1)
            {
                Text = GetMember(DisplayMemberPath);
            }
            Trace.WriteLine($"{DateTime.Now} - ReferenceComboBox_GotFocus: Text: {Text}.");
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
            if (!(e.OriginalSource is TextBox))
            {
                return;
            }
            IsDropDownOpen = false;
            SetSelectedIndexOnLostFocus();
            ItemsSource = originalItemsSource;
            RestoreOldValue();
            if (SelectedIndex == -1 || Text != (SelectedItem as DataRowView)?[DisplayMemberPath] as string)
            {
                Text = string.Empty;
                SelectedValue = string.Empty;
                Trace.WriteLine($"{DateTime.Now} - ReferenceComboBox_LostFocus: Text: {Text}.");
            }
            if (!string.IsNullOrEmpty(SelectedValueMemberPath))
            {
                TextSearch.SetTextPath(this, SelectedValueMemberPath);
            }
            SetSelectedValueMemberPath();
            var bindingExpression = GetBindingExpression(SelectedValueProperty);
            bindingExpression?.UpdateSource();
        }

        private void SetSelectedIndexOnLostFocus()
        {
            if (string.IsNullOrEmpty(Text))
            {
                return;
            }
            if (SelectedItem is DataRowView selectedItem && selectedItem[DisplayMemberPath].Equals(Text))
            {
                return;
            }
            FilterItemsSource();
            var count = ItemsSource.OfType<DataRowView>().Count();
            if (count == 1)
            {
                SelectedIndex = 0;
            }
        }

        private void RestoreOldValue()
        {
            if (!MustOldValueBeRestored())
            {
                oldSelectedIndex = -1;
                return;
            }
            SelectedIndex = oldSelectedIndex;
            oldSelectedIndex = -1;
            Text = ((DataRowView)SelectedItem)[DisplayMemberPath].ToString();
            Trace.WriteLine($"{DateTime.Now} - RestoreOldValue: Text: {Text}.");
        }

        private bool MustOldValueBeRestored()
        {
            if (!hasCustomCodes)
            {
                return false;
            }
            if (oldSelectedIndex.Equals(-1))
            {
                return false;
            }
            if (SelectedIndex.Equals(-1))
            {
                return true;
            }
            return !Text.Equals((SelectedItem as DataRowView)?[DisplayMemberPath] as string);
        }

        private void ReferenceComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Up:
                case Key.Down:
                    if (!IsDropDownOpen)
                    {
                        FilterItemsSource(false);
                    }
                    return;
                case Key.Tab:
                case Key.LeftShift:
                case Key.RightShift:
                case Key.Left:
                case Key.Right:
                case Key.PageUp:
                case Key.PageDown:
                case Key.Home:
                case Key.End:
                    return;
                case Key.Escape:
                    var bindingExpression = GetBindingExpression(SelectedValueProperty);
                    bindingExpression?.UpdateTarget();
                    return;
                case Key.Enter:
                    break;
                default:
                    if (OpenDropDownOnEnterKeyOnly && !IsDropDownOpen)
                    {
                        return;
                    }
                    break;
            }
            OpenDropDown(e);
        }

        private void OpenDropDown(KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                SelectedIndex = -1;
            }
            if (IsDropDownOpen)
            {
                FilterItemsSource();
            }
            IsDropDownOpen = true;
            if (e.OriginalSource is TextBox item)
            {
                item.Select(Text.Length, 0);
            }
        }

        private void ReferenceComboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (!HasEffectiveKeyboardFocus)
            {
                Text = GetMember(DisplayMemberPath);
                Trace.WriteLine($"{DateTime.Now} - ReferenceComboBox_DropDownOpened: Text: {Text}.");
            }
            SetOldSelectedIndex();
            FilterItemsSource();
        }

        private void SetOldSelectedIndex()
        {
            if (!hasCustomCodes)
            {
                return;
            }
            if (!oldSelectedIndex.Equals(-1))
            {
                return;
            }
            if (ItemsSource.Equals(originalItemsSource))
            {
                oldSelectedIndex = SelectedIndex;
            }
        }

        private void ReferenceComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ItemsSource = originalItemsSource;
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

        private void FilterItemsSource(bool filterText = true)
        {
            var originalText = Text;
            Trace.WriteLine($"{DateTime.Now} - FilterItemsSource Start: originalText: {originalText}.");
            var items = ItemsSource.Cast<DataRowView>().ToArray();
            if (filterText && items.Length > 0)
            {
                filterText = !(SelectedIndex >= 0 && Text.Equals(items[SelectedIndex][DisplayMemberPath]));
            }
            ItemsSource = originalItemsSource
                .Cast<DataRowView>()
                .Where(r => FilterCustomCodes(r) && FilterText(r, filterText));
            Text = originalText;
            Trace.WriteLine($"{DateTime.Now} - FilterItemsSource End: Text: {Text}.");
        }

        private bool FilterCustomCodes(DataRowView row)
        {
            if (!hasCustomCodes)
            {
                return true;
            }
            var code = row[SelectedValuePath].ToString();
            return customCodes.Any(c => c.Equals(code));
        }

        private bool FilterText(DataRowView row, bool filterText)
        {
            if (!filterText)
            {
                return true;
            }

            if (string.IsNullOrEmpty(Text))
            {
                return true;
            }
            var value = row[DisplayMemberPath].ToString();
            switch (FilterMethod)
            {
                case FilterMethods.Contains:
                    return value.IndexOf(Text, StringComparison.InvariantCultureIgnoreCase) > -1;
                case FilterMethods.StartsWith:
                    return value.StartsWith(Text, StringComparison.InvariantCultureIgnoreCase);
                default:
                    return false;
            }
        }

        public ICommand ComboBoxMouseDoubleClick => new ActionCommand<object>(ComboBoxMouseDoubleClickAction, param => true);

        private void ComboBoxMouseDoubleClickAction(object obj)
        {
            if (IsReadOnly)
            {
                return;
            }
            FilterItemsSource(false);
            var dialog = new DataGridPopup(ItemsSource);
            var dialogResult = dialog.ShowDialog() ?? false;
            if (dialogResult)
            {
                SelectedValue = dialog.SelectedValue;
            }
        }
    }
}
