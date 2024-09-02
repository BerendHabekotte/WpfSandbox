using BOSSAutoRef.Factory;
using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class BossCustomUnitTextBox : TextBox, INotifyPropertyChanged
    {
        public BossCustomUnitTextBox()
        {
            Units = new DataTable();
            Loaded += BossCustomUnitTextBox_Loaded;
            KeyDown += BossCustomUnitTextBox_KeyDown;
        }

        static BossCustomUnitTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BossCustomUnitTextBox),
                new FrameworkPropertyMetadata(
                    typeof(BossCustomUnitTextBox)));
            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(
                typeof(BossCustomUnitTextBox),
                new FrameworkPropertyMetadata(KeyboardNavigationMode.Local));
        }

        public AutoRefTypes AutoRefType
        {
            get => (AutoRefTypes)GetValue(AutoRefTypeProperty);
            set
            {
                SetValue(AutoRefTypeProperty, value);
                OnPropertyChanged("AutoRefType");
            }
        }

        public static readonly DependencyProperty AutoRefTypeProperty =
            DependencyProperty.Register(
                "AutoRefType",
                typeof(AutoRefTypes),
                typeof(BossCustomUnitTextBox),
                new PropertyMetadata(AutoRefTypes.NONE, null));

        public static readonly DependencyProperty AmountTextValueProperty =
            DependencyProperty.Register(
                "AmountValue",
                typeof(string),
                typeof(BossCustomUnitTextBox),
                 new PropertyMetadata("0.00", null));

        public static readonly DependencyProperty AmountUnitValueProperty =
            DependencyProperty.Register(
                "AmountUnitValue",
                typeof(string),
                typeof(BossCustomUnitTextBox),
                new PropertyMetadata("", null));

        public static readonly DependencyProperty SelectedUnitProperty =
            DependencyProperty.Register(
                "SelectedUnit",
                typeof(string),
                typeof(BossCustomUnitTextBox),
                new PropertyMetadata(string.Empty, null));

        public string AmountValue
        {
            get => (string)GetValue(AmountTextValueProperty);
            set => SetValue(AmountTextValueProperty, value);
        }

        public string AmountUnitValue
        {
            get => (string)GetValue(AmountUnitValueProperty);
            set => SetValue(AmountUnitValueProperty, value);
        }

        public string SelectedUnit
        {
            get => (string)GetValue(SelectedUnitProperty);
            set => SetValue(SelectedUnitProperty, value);
        }

        private bool amountUnitBoxFocus;
        public bool AmountUnitBoxFocus
        {
            get => amountUnitBoxFocus;
            set
            {
                amountUnitBoxFocus = value;
                OnPropertyChanged();
            }
        }

        private DataTable units;
        public DataTable Units
        {
            get => units;
            set
            {
                units = value;
                OnPropertyChanged();
            }
        }

        private int amountUnitZIndex;
        public int AmountUnitZIndex
        {
            get { return amountUnitZIndex; }
            set
            {
                amountUnitZIndex = value;
                OnPropertyChanged("AmountUnitZIndex");
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Units = Builder.GetInstance().GetRefDataView(AutoRefType, "", "", "", false).ToTable();
            ShowAmountUnitTextBoxTogether();
        }

        private void BossCustomUnitTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly)
                return;

            if (e.Key == Key.Tab)
            {
                if (!(e.OriginalSource is TextBox item))
                {
                    return;
                }
                else
                {
                    item.SelectAll();
                }
                    

                if (item.Name != "AmountTextBox")
                    return;
                ValidateAmount(e, item, "AmountTextBox");
            }

            if (e.Key != Key.Escape)
                return;

            BindingExpression beSU = GetBindingExpression(SelectedUnitProperty);
            BindingExpression be = GetBindingExpression(AmountTextValueProperty);
            if (be == null)
                return;
            be.UpdateTarget();
            if (beSU == null)
                return;
            beSU.UpdateTarget();

        }

        private void BossCustomUnitTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            PreviewGotKeyboardFocus += BossCustomUnitTextBox_PreviewGotKeyboardFocus;
            PreviewLostKeyboardFocus += BossCustomUnitTextBox_PreviewLostKeyboardFocus;
            LostFocus += BossCustomUnitTextBox_LostFocus;
        }

        private void PerformValidation(RoutedEventArgs e, Control oldItem)
        {
            switch (oldItem.Name)
            {
                case "PART_EditableTextBox":
                case "AmountTextBox":
                    {
                        ValidateAmount(e, oldItem, oldItem.Name);
                        break;
                    }
            }
        }

        private void BossCustomUnitTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsReadOnly)
                return;
            if (!(e.OriginalSource is Control oldItem))
                return;
            PerformValidation(e, oldItem);
        }


        private void BossCustomUnitTextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsReadOnly)
                return;
            if (!(e.OldFocus is Control oldItem))
                return;

            PerformValidation(e, oldItem);
            ShowAmountUnitTextBoxTogether();
        }

        private void ShowAmountUnitTextBoxTogether()
        {
            AmountUnitZIndex = 100;
        }

        private void ShowAmountAndUnitComboBoxTogether()
        {
            AmountUnitZIndex = -100;
        }

        private void ValidateAmount(RoutedEventArgs e, Control item, string callerName)
        {
            if (!(item is TextBox txtBox))
                return;
            if (callerName == "AmountTextBox")
                AmountValue = txtBox.Text;
            else
                SelectedUnit = txtBox.Text;
            if (!Validation.GetHasError(this))
                return;

            txtBox.SelectAll();
            e.Handled = true;
            var errors = Validation.GetErrors(this);
            //BossLibrary.MainLibrary.ShowErrors(errors[0].Exception);
        }

        private void BossCustomUnitTextBox_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsReadOnly)
                return;
            if (!(e.NewFocus is Control item))
                return;
            AmountUnitBoxFocus = Keyboard.PrimaryDevice.IsKeyDown(Key.Tab) ? false : true;
            SetControlVisibility(item);
        }

        private void SetControlVisibility(Control item)
        {
            switch (item.Name)
            {
                case "AmountUnitTextBox":
                case "UnitCombo":
                case "AmountTextBox":
                case "PART_EditableTextBox":
                    if ((item is TextBox txtBox))
                        txtBox.SelectAll();
                    ShowAmountAndUnitComboBoxTogether();
                    return;

                default:
                    ShowAmountUnitTextBoxTogether();
                    return;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
