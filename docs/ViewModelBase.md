# ViewModelBase
Implmentation of `INotifyPropertyChanged` for property change notifications in MVVM.

## How to use
Simply inherit from `ViewModelBase` in your ViewModel classes. It provides the needed functionality to raise property change notifications.

```
namespace ExampleProject.ViewModels
{
	public class ExampleViewModel : ViewModelBase
	{
		private int _myNumValue;
		public int MyNumValue
		{
			get => _myNumValue;
			set
			{
				RaiseAndSetIfChanged(ref _myNumValue, value);
				RaisePropertyChanged(nameof(DoubleOutput)); // Notify that DoubleOutput has changed
			}
		}

		public int DoubleOutput => MyNumValue * 2;
	}
}
```