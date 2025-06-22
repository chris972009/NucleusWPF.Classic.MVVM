using System;
using System.Windows;

namespace NucleusWPF.MVVM
{
    /// <summary>
    /// Defines methods for displaying messages to the user.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Displays a message box with the specified message, title, button options, and icon.
        /// </summary>
        /// <param name="message">The text to display in the message box.</param>
        /// <param name="title">The title of the message box window.</param>
        /// <param name="button">The button options to display in the message box, such as OK or Cancel.</param>
        /// <param name="icon">The icon to display in the message box, such as an information or error icon.</param>
        void Show(string message, string title, MessageBoxButton button, MessageBoxImage icon);

        /// <summary>
        /// Displays exception message to the user.
        /// </summary>
        /// <param name="ex">Exception to display</param>
        void Show(Exception ex);
    }
}
