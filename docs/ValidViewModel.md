# ValidViewModel
Provides an implementation of `INotifyDataErrorInfo` and extends `ViewModelBase` for validation scenarios in MVVM applications.

## How to use
Inherit the 'ValidViewModel' class in your ViewModel. Adding or removing errors from a property will notify the view.

```
namespace ExampleProject.ViewModels
{
	public class ExampleViewModel : ValidViewModel
	{
		private int numInput;
		public int NumInput
		{
			get => numInputs;
			set => RaiseAndValidateIfChanged(ref numInput, value, ValidateExceedsZero);
		}

		private void ValidateExceedsZero(int value, string? propertyName)
		{
			ClearErrors(propertyName);
			if (value <= 0) AddError("Value must be greater than zero.", propertyName);
		}
	}
}
```
