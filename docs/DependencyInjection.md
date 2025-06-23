# DependencyInjection

The `DependencyInjection` class in `NucleusWPF.MVVM` provides a simple dependency injection container for services and view models in your WPF application. It supports both transient and singleton lifetimes.

---

## Registering Services

By default, build int services are already registered such as `IMessageService`. If you need to register additional services, there are a few ways to do so.

### Transient/Scoped Services

In the "App.xaml.cs" file, you can register services using the Reegister method.
```
DependencyInjection.Instance.Register<IServiceName, ServiceName>();
```

### Singleton Services

"In the "App.xaml.cs" file, you ster singleton services using the RegisterSingleton methods.
```
DendencyInjection.Instance.RegisterSingleton<IServiceName, ServiceName>();
```

Alternatively, you can use the RegisterSingleton method with an instance of the service:
```
DependencyInjection.Instance.RegisterSingleton<IServiceName>(new ServiceName());
```

## Retrieving Services & ViewModels

Services and ViewModels are retreed using the same method.
When ViewModels are initialized this way, DependencyInjection container will automatically resolve any dependencies it contains.

```
var service = DependencyInjection.Instance.Resolve<IServiceName>();
var viewModel = DependencyInjection.Instance.Resolve<ViewModelName>();
```

To ensure ensure ViewModels are properly initialized, the constructor should contains all required dependencies. If there is more than one constructor, the container will use the one with the most parameters.

```
public class ExampleViewMode(IMessageService _messageService) : ViewModelBase
{
	public void SendMessage()
	{
		_messageService.Show("Hello World!", "Hello", MessageBoxButton.OK, MessageBoxImage.Information);
	}
}
```