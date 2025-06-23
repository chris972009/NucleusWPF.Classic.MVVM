# IWindowService
Interface for managing windows in a WPF application. Allows for loose coupling between Views and ViewModels.

## Mapping ViewModels to Views
Although `IWindowService` uses implicit mapping based on MVVM naming conventions. A ViewModel can be registered to a View explicitly using the Register method.

```
var messageService = DependencyInjection.Instance.Resolve<IWindowService>();
messageService.Register<TViewModel, TWindow>();
```

## Opening a Window
Similar to WPF bulit in method. `IWindowService` provides two methods for opening windows.

### Show Method
If you want to open a window but don't want the calling window to bocked by the new one use Show method.

```
_windowService.Show<ExampleViewMode>();
```

Alternatively if you need to pass data to the ViewModel the following method can be used.

```
var viewModel = DependencyInjection.Instance.Resolve<ExampleViewModel>();
viewModel.Data = "Here is some sample data";
_windowService.Show(viewModel);
```

### ShowDialog Method
If you want to open a window and wait for it to be closed before continuing use ShowDialog method.

```
_windowService.ShowDialog<ExampleViewMode>();
```

Alternatively if you need to pass or read data from the ViewModel the following method can be used.
```
var viewModel = DependencyInjection.Instance.Resolve<ExampleViewModel>();
viewModel.Data = "Here is some sample data";
if (_windowService.ShowDialog(viewModel) == true)
{
	// Do something with the result
	var resultData = viewModel.ResultData;
}
```