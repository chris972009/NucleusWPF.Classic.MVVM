using System;
using System.Threading;
using System.Threading.Tasks;

namespace NucleusWPF.Classic.MVVM
{
    /// <summary>
    /// Implmentation of ICommand that supports asynchronous execution.
    /// </summary>
    public sealed class AsyncRelayCommand : IRelayCommand
    {
        /// <summary>
        /// Initializes a new isntance of <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="execute">Action to be executed when invoked.</param>
        /// <param name="canExecute">Determines whether the command can execute.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class with the specified asynchronous
        /// execute action.
        /// </summary>
        /// <remarks>This constructor creates an <see cref="AsyncRelayCommand"/> that is always enabled. 
        /// Use the overload with a <c>canExecute</c> predicate to specify conditions under which the command is
        /// enabled.</remarks>
        /// <param name="execute">Action to be executed when invoked.</param>
        public AsyncRelayCommand(Func<Task> execute) : this(execute, () => true)
        {

        }

        private int _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter) =>
            _isExecuting == 0 && _canExecute();

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        public async Task ExecuteAsync()
        {
            if (!_canExecute()) return;
            if (Interlocked.CompareExchange(ref _isExecuting, 1, 0) != 0) return;
            RaiseCanExecuteChanged();
            try
            {
                await _execute();
            }
            finally
            {
                Interlocked.Exchange(ref _isExecuting, 0);
                RaiseCanExecuteChanged();
            }
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            ExecuteAsync().ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    var messageService = DependencyInjection.Instance.Resolve<IMessageService>();
                    messageService.Show(t.Exception.InnerException ?? t.Exception);
                }
            },
            TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Implentation of ICommand that supports asynchronous execution.
    /// </summary>
    /// <typeparam name="T">Specifies the type for command parameter.</typeparam>
    public sealed class AsyncRelayCommand<T>
    {
        /// <summary>
        /// Initializes a new isntance of <see cref="AsyncRelayCommand"/> class.
        /// </summary>
        /// <param name="execute">Action to be executed when invoked.</param>
        /// <param name="canExecute">Determines whether the command can execute.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
            _canExecuteParameterRequired = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class with the specified asynchronous
        /// execute action.
        /// </summary>
        /// <remarks>This constructor creates an <see cref="AsyncRelayCommand"/> where CanExecute does not require a parameter.
        /// <see cref="CanExecute(object?)"/> does not require a parameter.</remarks>
        /// <param name="execute">Action to be executed when invoked.</param>
        /// <param name="canExecute">Predicate to determine whether the command can execute.</param>
        public AsyncRelayCommand(Func<T, Task> execute, Func<bool> canExecute) : this(execute, (param) => canExecute())
        {
            _canExecuteParameterRequired = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class with the specified asynchronous
        /// execute action.
        /// </summary>
        /// <remarks>This constructor creates an <see cref="AsyncRelayCommand"/> that is always enabled. 
        /// Use the overload with a <c>canExecute</c> predicate to specify conditions under which the command is
        /// enabled.</remarks>
        /// <param name="execute">Action to be executed when invoked.</param>
        public AsyncRelayCommand(Func<T, Task> execute) : this(execute, (param) => true)
        {
            _canExecuteParameterRequired = false;
        }

        private readonly bool _canExecuteParameterRequired;
        private int _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            if (_isExecuting != 0) return false;
            
            if (_canExecuteParameterRequired)
                return parameter is T param && _canExecute(param);

            return _canExecute(default);
        }

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="t">The parameter to be passed to the execute action.</param>
        public async Task ExecuteAsync(T t)
        {
            if (!_canExecute(t)) return;
            if (Interlocked.CompareExchange(ref _isExecuting, 1, 0) != 0) return;
            RaiseCanExecuteChanged();
            try
            {
                await _execute(t);
            }
            finally
            {
                Interlocked.Exchange(ref _isExecuting, 0);
                RaiseCanExecuteChanged();
            }
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            Exception ex = null;

            if(parameter is T value)
            {
                ExecuteAsync(value).ContinueWith(t =>
                {
                    if (t.Exception != null)
                        ex = t.Exception.InnerException ?? t.Exception;
                },
                TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                ex = new ArgumentException($"Pmeter must be of type {typeof(T).Name}", nameof(parameter));
            }

            if (ex != null)
            {
                var messageService = DependencyInjection.Instance.Resolve<IMessageService>();
                messageService.Show(ex);
            }
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
