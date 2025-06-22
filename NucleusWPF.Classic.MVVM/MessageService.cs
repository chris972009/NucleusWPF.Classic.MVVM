using System;
using System.Windows;

namespace NucleusWPF.MVVM
{
    /// <summary>
    /// Provides methods for displaying messages and exceptions to the user using WPF message boxes.
    /// Implements the <see cref="IMessageService"/> interface.
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// Displays a message box with the specified message, title, button options, and icon.
        /// </summary>
        /// <param name="message">The text to display in the message box.</param>
        /// <param name="title">The title of the message box window.</param>
        /// <param name="button">The button options to display in the message box, such as OK or Cancel.</param>
        /// <param name="icon">The icon to display in the message box, such as an information or error icon.</param>
        public virtual void Show(string message, string title, MessageBoxButton button, MessageBoxImage icon) =>
            MessageBox.Show(message, title, button, icon);

        /// <summary>
        /// Displays an exception message to the user in a message box.
        /// </summary>
        /// <param name="ex">The exception to display.</param>
        public virtual void Show(Exception ex) =>
            MessageBox.Show(ex.GetType().Name, "Unhanlded Exception", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
