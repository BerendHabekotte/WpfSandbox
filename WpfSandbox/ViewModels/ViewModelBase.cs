using System;
using System.ComponentModel;
using System.Diagnostics;

namespace WpfSandbox.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable 
    {
        public virtual string DisplayName { get; set; }

        public bool ThrowOnInvalidPropertyName { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            if (handler == null)
            {
                return;
            }
            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var message = $"Invalid property name: {propertyName}";
                if (ThrowOnInvalidPropertyName)
                {
                    throw new Exception(message);
                }
                else
                {
                    Debug.Fail(message);
                }
            }
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}
