using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NucleusWPF.Classic.MVVM
{
    /// <summary>
    /// Implementation of INotifyPropertyChanged.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">Name of changed property.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Updates the specified property with a new value and raises a property change notification if the value has
        /// changed.
        /// </summary>
        /// <typeparam name="T">The type of the property being updated.</typeparam>
        /// <param name="targetProperty">A reference to the backing field of the property to update.</param>
        /// <param name="value">The new value to assign to the property.</param>
        /// <param name="propertyName">The name of the property to raise the change notification for. This is automatically provided by the
        /// compiler if not explicitly specified, using the caller's member name.</param>
        protected void RaiseAndSetIfChanged<T>(ref T targetProperty, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(targetProperty, value))
            {
                targetProperty = value;
                RaisePropertyChanged(propertyName);
            }
        }
    }
}
