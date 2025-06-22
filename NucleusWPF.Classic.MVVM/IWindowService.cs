using System.Windows;

namespace NucleusWPF.MVVM
{
    /// <summary>
    /// Provides functionality for creating new windows.
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Gets or sets the default suffix appended to view names.
        /// </summary>
        string DefaultViewSuffix { get; set; }

        /// <summary>
        /// Registers an explicit mapping between aa View and its ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel to register.</typeparam>
        /// <typeparam name="TWindow">View to map.</typeparam>
        void Register<TViewModel, TWindow>() where TViewModel : class where TWindow : Window;

        /// <summary>
        /// Opens a View with its datacontext as its assosiated ViewModel.
        /// </summary>
        /// <param name="viewModel">Assosiated ViewModel</param>
        /// <param name="suffix">Suffix for View, <see cref="DefaultViewSuffix"/> is used if null.</param>
        void Show(object viewModel, string suffix = null);

        /// <summary>
        /// Opens a View with its datacontext as its assosiated ViewModel.
        /// </summary>
        /// <param name="viewModel">Assosiated ViewModel</param>
        /// <param name="suffix">Suffix for View, <see cref="DefaultViewSuffix"/> is used if null.</param>
        /// <returns>Returns the DialogResult of view</returns>
        bool? ShowDialog(object viewModel, string suffix = null);

        /// <summary>
        /// OPens a View with its datacontext as its assosiated ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel">Assosiated ViewModel as type</typeparam>
        /// <param name="suffix">Suffix for View, <see cref="DefaultViewSuffix"/> if null is used</param>
        void Show<TViewModel>(string suffix = null) where TViewModel : class;

        /// <summary>
        /// OPens a View with its datacontext as its assosiated ViewModel.
        /// </summary>
        /// <typeparam name="TViewModel">Assosiated ViewModel as type</typeparam>
        /// <param name="suffix">Suffix for View, <see cref="DefaultViewSuffix"/> if null is used</param>
        /// <returns>Returns the DialogResult of view</returns>
        bool? ShowDialog<TViewModel>(string suffix = null) where TViewModel : class;
    }
}
