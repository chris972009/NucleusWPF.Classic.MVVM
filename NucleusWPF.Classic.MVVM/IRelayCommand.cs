using System.Windows.Input;

namespace NucleusWPF.MVVM
{
    /// <summary>
    /// Represents a command that can be executed and provides a mechanism to notify changes in its ability to execute.
    /// </summary>
    /// <remarks>This interface extends <see cref="ICommand"/> by adding the <see
    /// cref="RaiseCanExecuteChanged"/> method,  which allows manual triggering of the <see
    /// cref="ICommand.CanExecuteChanged"/> event. This is useful in scenarios  where the command's ability to execute
    /// depends on external conditions that may change dynamically.</remarks>
    public interface IRelayCommand : ICommand
    {
        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event to indicate that the result of the <see cref="CanExecute"/>
        /// method may have changed."/>
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
