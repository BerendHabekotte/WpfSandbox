using BcWpfCommon.Commands;
using BOSSAutoRef.Factory;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class BossCustomComboBox : ComboBox, INotifyPropertyChanged
    {
        private IEnumerable dataView;

        static BossCustomComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BossCustomComboBox), new FrameworkPropertyMetadata(typeof(BossCustomComboBox)));
        }

        public BossCustomComboBox()
        {
            ItemSourceCollection = new ObservableCollection<string>();
            Loaded += BossCustomComboBox_Loaded;
            KeyDown += BossCustomComboBox_KeyDown;
        }

        public static readonly DependencyProperty AutoRefTypeProperty =
            DependencyProperty.Register(
                "AutoRefType",
                typeof(AutoRefTypes),
                typeof(BossCustomComboBox),
                new PropertyMetadata(AutoRefTypes.NONE, null));

        public static readonly DependencyProperty AutoRefFilterProperty =
            DependencyProperty.Register(
                "AutoRefFilter",
                typeof(string),
                typeof(BossCustomComboBox),
                new PropertyMetadata("", null));

        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register(
                "CustomComboBoxText",
                typeof(string),
                typeof(BossCustomComboBox),
                new UIPropertyMetadata(null));

        public string CustomComboBoxText
        {
            get => (string)GetValue(TextValueProperty);
            set => SetValue(TextValueProperty, value);
        }

        public AutoRefTypes AutoRefType
        {
            get => (AutoRefTypes)GetValue(AutoRefTypeProperty);
            set
            {
                SetValue(AutoRefTypeProperty, value);
                OnPropertyChanged();
            }
        }

        public string AutoRefFilter
        {
            get => (string)GetValue(AutoRefFilterProperty);
            set
            {
                SetValue(AutoRefFilterProperty, value);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> itemSourceCollection;

        public ObservableCollection<string> ItemSourceCollection
        {
            get => itemSourceCollection;
            set
            {
                itemSourceCollection = value;
                OnPropertyChanged();
            }
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
                OnPropertyChanged(nameof(IsPopupOpen));
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

        public ICommand ComboBoxMouseDoubleClick
        {
            get => new ActionCommand<object>(ComboBoxMouseDoubleClickAction, param => true);
        }

        public ICommand PopUpGridSelectedItemDoubleClick
        {
            get => new ActionCommand<object>(PopUpGridSelectedItemDoubleClickAction, param => true);
        }

        public ICommand ClosePopup
        {
            get => new ActionCommand<object>(ClosePopupClickAction, param => true);
        }

        private void ClosePopupClickAction(object obj)
        {
            IsPopupOpen = false;
        }

        private void ComboBoxMouseDoubleClickAction(object obj)
        {
            if (IsReadOnly)
                return;

            // TODO DI
            var dialog = new DataGridPopup(dataView);
            var dialogResult = dialog.ShowDialog() ?? false;
            if (dialogResult)
            {
                CustomComboBoxText = dialog.SelectedValue;
            }
        }

        private void PopUpGridSelectedItemDoubleClickAction(object obj)
        {
            CustomComboBoxText = PopUpGridSelectedValue;
            IsPopupOpen = false;
        }

        private void BossCustomComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (AutoRefType == AutoRefTypes.NONE)
                return;

            if (AutoRefType != AutoRefTypes.TYPE_BrokStatusValue)
                return;
            if (AutoRefFilter != "CT")
            {
                return;
            }
            var itemsSource = Builder.GetInstance().GetRefDataView(AutoRefType, "", AutoRefFilter, "", false);

            AddCustomDescriptionColumn(itemsSource);

            foreach (DataRow row in itemsSource.Table.Rows)
            {
                FillCustomDescriptionField(row);
            }

            PreviewLostKeyboardFocus += BossCustomComboBox_PreviewLostKeyboardFocus;
            LostFocus += BossCustomComboBox_LostFocus;
            KeyDown += BossCustomComboBox_KeyDown;
            KeyUp += BossCustomComboBox_KeyUp;
            IsPopupOpen = false;
            try
            {
                dataView = ItemsSource = itemsSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BossCustomComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsReadOnly)
                return;

            if (!(e.OriginalSource is TextBox item))
                return;

            IsDropDownOpen = false;

            if (ItemsSource == null)
                CustomComboBoxText = string.Empty;

            ValidateStatus(e, item);
        }

        private void BossCustomComboBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsReadOnly)
                return;

            if (!(e.OldFocus is TextBox item))
                return;

            IsDropDownOpen = true;

            ValidateStatus(e, item);
        }

        private void ValidateStatus(RoutedEventArgs e, TextBox txtBox)
        {
            if (txtBox.Name != "PART_EditableTextBox")
                return;
            CustomComboBoxText = txtBox.Text;
            if (!Validation.GetHasError(this))
                return;

            e.Handled = true;
            var errors = Validation.GetErrors(this);
        }

        private void BossCustomComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
                return;

            if (e.Key == Key.Tab)
            {
                if (!(e.OriginalSource is TextBox item))
                    return;

                ValidateStatus(e, item);

                return;
            }

            if (e.Key != Key.Escape)
                return;

            BindingExpression be = GetBindingExpression(TextValueProperty);
            if (be == null)
                return;

            be.UpdateTarget();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BossCustomComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.OriginalSource is TextBox item))
                return;

            SearchText = item.Text;
            IsDropDownOpen = true;
            ValidateStatus(e, item);
        }

        private void FilterItemsSource()
        {
            if (string.IsNullOrEmpty(SearchText))
                ItemsSource = dataView;
            else
                ItemsSource = GetFilteredItemSource();
        }

        private IEnumerable GetFilteredItemSource()
        {
            foreach (DataRowView itemSource in dataView)
            {
                if (itemSource[this.DisplayMemberPath].ToString().IndexOf(SearchText, StringComparison.InvariantCultureIgnoreCase) > -1)
                    yield return itemSource;
            }
        }

        private void AddCustomDescriptionColumn(DataView itemsSource)
        {
            if (!itemsSource.Table.Columns.Contains("CustomDescription"))
                itemsSource.Table.Columns.Add("CustomDescription", typeof(string));
        }
        private void FillCustomDescriptionField(DataRow row)
        {
            StringBuilder builder = new StringBuilder();

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

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();

                FilterItemsSource();
            }
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
                typeof(BossCustomComboBox),
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
                typeof(BossCustomComboBox),
                new PropertyMetadata(new PropertyChangedCallback(WatermarkHorizontalAlignmentChanged)));

        private static void WatermarkHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

    }
}