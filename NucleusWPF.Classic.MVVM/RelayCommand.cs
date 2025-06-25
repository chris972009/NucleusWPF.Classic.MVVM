using System;

namespace NucleusWPF.Classic.MVVM
{
    /// <summary>
    /// Implementation of <see cref="IRelayCommand"/> that supports synchronous execution.
    /// </summary>
    public sealed class RelayCommand : IRelayCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class with the specified execute action and can-execute predicate.
        /// </summary>
        /// <param name="execute">Action to be executed when invoked.</param>
        /// <param name="canExecute">Determines whether the command can execute.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class with the specified execute action.
        /// </summary>
        /// <remarks>
        /// This constructor creates a <see cref="RelayCommand"/> that is always enabled.
        /// Use the overload with a <c>canExecute</c> predicate to specify conditions under which the command is enabled.
        /// </remarks>
        /// <param name="execute">Action to be executed when invoked.</param>
        public RelayCommand(Action execute) : this(execute, () => true)
        {

        }

        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter) => _canExecute();

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (!_canExecute()) return;
            try
            {
                _execute();
            }
            catch (Exception ex)
            {
                var messageService = DependencyInjection.Instance.Resolve<IMessageService>();
                messageService.Show(ex);
            }
        }               

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Implementation of <see cref="IRelayCommand"/> that supports synchronous execution with a strongly-typed parameter.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    public sealed class RelayCommand<T> : IRelayCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class with the specified execute action and can-execute predicate.
        /// </summary>
        /// <param name="execute">Action to be executed when invoked.</param>
        /// <param name="canExecute">Determines whether the command can execute for the given parameter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class with the specified execute action and can-execute predicate.
        /// </summary>
        /// <param name="execute">Action to be executed when invoked.</param>
        /// <param name="canExecute">Determines whether the command can execute.</param>
        public RelayCommand(Action<T> execute, Func<bool> canExecute) :
            this(execute, _ => canExecute())
        {
            _parameterRequired = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand{T}"/> class with the specified execute action.
        /// </summary>
        /// <remarks>
        /// This constructor creates a <see cref="RelayCommand{T}"/> that is always enabled.
        /// Use the overload with a <c>canExecute</c> predicate to specify conditions under which the command is enabled.
        /// </remarks>
        /// <param name="execute">Action to be executed when invoked.</param>
        public RelayCommand(Action<T> execute) : this(execute, _ => true)
        {
            _parameterRequired = false;
        }

        private readonly bool _parameterRequired = true;

        private readonly Action<T> _execute;

        private readonly Func<T, bool> _canExecute;

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            if (!_parameterRequired) return _canExecute(default);
            else if (parameter is T t) return _canExecute(t);
            else return false;
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            try
            {
                if (parameter is T t)
                {
                    if (!_canExecute(t)) return;
                    _execute(t);
                }
                else
                {
                    throw new ArgumentException($"Parameter must be of type {typeof(T).Name}", nameof(parameter));
                }
            }
            catch (Exception ex)
            {
                var messageService = DependencyInjection.Instance.Resolve<IMessageService>();
                messageService.Show(ex);
            }
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
